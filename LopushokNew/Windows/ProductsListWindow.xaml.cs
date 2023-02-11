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
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public Dictionary<string, Func<Product, object>> Sortings { get; set; }

        public ProductsListWindow()
        {
            InitializeComponent();

            Products = DataAccess.GetProducts();

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
        }
    }
}
