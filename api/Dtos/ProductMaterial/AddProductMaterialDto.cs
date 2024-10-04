using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.ProductMaterial
{
    public class AddProductMaterialDto
    {   
        public decimal Percentage { get; set; } // Example: 100.0
        public int MaterialId { get; set; } // Foreign key for Material
    }
}