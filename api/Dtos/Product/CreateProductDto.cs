using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ProductVariant;

namespace api.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public int GenderId { get; set; }
        public List<CreateProductVariantDto> Variants { get; set; }
    }
}