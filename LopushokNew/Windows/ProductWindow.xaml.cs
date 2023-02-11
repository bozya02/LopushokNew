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
        }
    }
}
