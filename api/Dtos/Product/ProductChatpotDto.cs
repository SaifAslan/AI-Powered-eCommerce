using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.ProductMaterial;
using api.Dtos.ProductVariant;
using api.Models;

namespace api.Dtos.Product
{
    public class ProductChatpotDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public BrandDto Brand {get; set;}
        public SubCategoryDto SubCategory {get; set;}
        public CategoryDto Category {get; set;}
        public GenderDto Gender {get; set;}
        public List<ProductVariantChatpotDto> Variants { get; set; }
        public List<ProductMaterialChatpotDto> Materials { get; set; }
    }
}



