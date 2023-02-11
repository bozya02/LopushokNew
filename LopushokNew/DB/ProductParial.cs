using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LopushokNew.DB
{
    public partial class Product
    {
        public string MaterialsList => string.Join(", ", ProductMaterials.Select(x => x.Material.Name));

        public string Color => "#ffffff";
    }
}
