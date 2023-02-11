using LopushokNew.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LopushokNew.Windows
{
    /// <summary>
    /// Interaction logic for ProductsListWindow.xaml
    /// </summary>
    public partial class ProductsListWindow : Window
    {
        private const int ITEMONPAGE = 20;
        private int page { get; set; } = 0;

        private List<Product> _products { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public Dictionary<string, Func<Product, object>> Sortings { get; set; }

        public ProductsListWindow()
        {
            InitializeComponent();

            _products = DataAccess.GetProducts();
            Products = _products.ToList();

            ProductTypes = DataAccess.GetProductTypes();
            ProductTypes.Insert(0, new ProductType
            {
                Name = "Все типы"
            });

            Sortings = new Dictionary<string, Func<Product, object>>
            {
                { "Наименование по убыванию", x => x.Name },
                { "Наименование по возратсанию", x => x.Name }, //reverse
                { "Цех по убыванию", x => x.Name },
                { "Цех по возратсанию", x => x.Name },          //reverse
                { "Стоимость по убыванию", x => x.Name },
                { "Стоимость по возратсанию", x => x.Name },    //reverse
            };

            this.DataContext = this;

            DataAccess.AddNewItemEvent += DataAccess_AddNewItemEvent;

            ApplyFilters();
        }

        private void DataAccess_AddNewItemEvent()
        {
            _products = DataAccess.GetProducts();
            ApplyFilters();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbProducttype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters(bool isStartFilter = true)
        {
            var search = tbSearch.Text.ToLower().Trim();
            var sorting = cbSorting.SelectedItem as string;
            var productType = cbProductType.SelectedItem as ProductType;

            if (string.IsNullOrEmpty(sorting) || productType == null)
                return;

            if (isStartFilter)
                page = 0;

            Products = _products.FindAll(x => x.Name.ToLower().Contains(search) ||
                                              x.Description.ToLower().Contains(search));

            if (productType.Id != 0)
                Products = Products.FindAll(x => x.ProductType == productType);

            Products = (cbSorting.SelectedItem as string).Contains("убыванию") ?
                Products.OrderBy(Sortings[sorting]).ToList() :
                Products.OrderByDescending(Sortings[sorting]).ToList();

            lvProducts.ItemsSource = Products.Skip(page * ITEMONPAGE).Take(ITEMONPAGE);
            lvProducts.Items.Refresh();

            GeneratePages();
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var product = lvProducts.SelectedItem as Product;

            if (product != null)
                new ProductWindow(product).ShowDialog();

            lvProducts.SelectedIndex = -1;
        }

        private void Paginator(object sender, MouseButtonEventArgs e)
        {
            var content = (sender as TextBlock).Text.ToString();

            var pagesCount = Products.Count / ITEMONPAGE + (Products.Count % ITEMONPAGE != 0 ? 1 : 0);

            if (content.Contains("<") && page > 0)
                page--;
            else if (content.Contains(">") && page < pagesCount - 1)
                page++;
            else if (int.TryParse(content, out int pageNumber))
                page = pageNumber - 1;

            ApplyFilters(false);
        }

        private void GeneratePages()
        {
            spPages.Children.Clear();

            var pagesCount = Products.Count / ITEMONPAGE + (Products.Count % ITEMONPAGE != 0 ? 1 : 0);

            for (int i = 0; i < pagesCount; i++)
            {
                spPages.Children.Add(new TextBlock
                {
                    Text = (i + 1).ToString(),
                    Style = Application.Current.FindResource("Pagination") as Style
                });

                spPages.Children[i].PreviewMouseDown += Paginator;
            }
            if (spPages.Children.Count != 0)
                (spPages.Children[page] as TextBlock).TextDecorations = TextDecorations.Underline;
        }

        private void btnEditPrice_Click(object sender, RoutedEventArgs e)
        {
            var products = lvProducts.SelectedItems.Cast<Product>().ToList();
            if (products.Count == 0)
                return;

            new EditPriceWindow(products).ShowDialog();

            lvProducts.SelectedIndex = -1;
        }

        private void btnNewProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(new Product()).ShowDialog();
        }
    }
}
