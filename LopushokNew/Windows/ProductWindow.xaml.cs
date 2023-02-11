using LopushokNew.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public Product Product { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public List<Material> Materials { get; set; }

        public ProductWindow(Product product)
        {
            InitializeComponent();

            Product = product;
            ProductTypes = DataAccess.GetProductTypes();
            Materials = DataAccess.GetMaterials();

            this.DataContext = this;
        }

        private void cbMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var material = cbMaterials.SelectedItem as Material;

            if (material == null)
                return;

            Product.ProductMaterials.Add(new ProductMaterial
            {
                Product = Product,
                Material = material,
                MaterialQuantity = 0
            });

            lvMaterials.ItemsSource = Product.ProductMaterials;
            lvMaterials.Items.Refresh();
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "png|*.png|jpeg|*.jpeg|jpg|*.jpg"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);
                Product.Image = image;

                imgProduct.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Product.Name))
                stringBuilder.AppendLine("Наимнование не введено!");
            if (DataAccess.IsExist(Product))
                stringBuilder.AppendLine("Артикул не уникален!");
            if (Product.MinPrice <= 0)
                stringBuilder.AppendLine("Цена должно быть больше 0!");
            if (Product.PersonForProduction <= 0)
                stringBuilder.AppendLine("Количество челове для должно быть больше 0!");
            if (Product.Workshop <= 0)
                stringBuilder.AppendLine("Неверный номер цеха!");
            if (Product.ProductType == null)
                stringBuilder.AppendLine("Тип товара не выбран!");

            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString() , "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            DataAccess.SaveProduct(Product);
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
