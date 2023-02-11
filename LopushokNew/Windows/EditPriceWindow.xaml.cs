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
    /// Interaction logic for EditPriceWindow.xaml
    /// </summary>
    public partial class EditPriceWindow : Window
    {
        public List<Product> Products { get; set; }

        public EditPriceWindow(List<Product> products)
        {
            InitializeComponent();

            Products = products;

            tbPrice.Text = Math.Round(Products.Sum(x => x.MinPrice) / Products.Count(), 2).ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(tbPrice.Text, out price))
            {
                MessageBox.Show("Введено не число!", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            foreach (var product in Products)
            {
                product.MinPrice += price;
                DataAccess.SaveProduct(product);
            }
            
            this.Close();
        }
    }
}
