using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokNew.DB
{
    public class DataAccess
    {
        public delegate void AddNewItem();
        public static event AddNewItem AddNewItemEvent;

        private static DbSet<Product> _products => LopushokNewBozyaEntities.GetContext().Products;


        public static List<Product> GetProducts() => _products.ToList();
        public static List<Material> GetMaterials() => LopushokNewBozyaEntities.GetContext().Materials.ToList();
        public static List<ProductType> GetProductTypes() => LopushokNewBozyaEntities.GetContext().ProductTypes.ToList();

        public static void SaveProduct(Product product)
        {
            if (product.Id == 0)
                _products.Add(product);

            LopushokNewBozyaEntities.GetContext().SaveChanges();
            AddNewItemEvent?.Invoke();
        }

        public static bool IsExist(Product product)
        {
            return _products.Any(p => p.Article == product.Article && p.Id != product.);
        }
    }
}
