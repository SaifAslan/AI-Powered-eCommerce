using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Colour
    {
        public int Id { get; set; } // Unique identifier for the color
        public string Name { get; set; } // Example: "Red", "Blue"
        public ICollection<ProductColour> ProductColours { get; set; } // Navigation property for many-to-many
    }

}