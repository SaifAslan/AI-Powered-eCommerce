using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ProductMaterial
    {
        public int ProductId { get; set; } // Foreign key for Product
        public Product Product { get; set; } // Navigation property for Product

        public int MaterialId { get; set; } // Foreign key for Material
        public Material Material { get; set; } // Navigation property for Material
    }
}