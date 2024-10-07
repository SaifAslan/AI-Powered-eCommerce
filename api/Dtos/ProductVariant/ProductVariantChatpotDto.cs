using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Colour;
using api.Dtos.Size;
using api.Models;

namespace api.Dtos.ProductVariant
{
    public class ProductVariantChatpotDto
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public SizeDto Size { get; set;}
        public ColourDto Colour { get; set; }
    }
}