using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Material
    {
        public int Id { get; set; } // Unique identifier for the material
        public string Name { get; set; } // Example: "Cotton", "Polyester"
        public ICollection<ProductMaterial> ProductMaterials { get; set; } // Navigation property for many-to-many
    }

}