
using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDto ToGetProductsDto(this Product product, ProductQueryObject filter)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsFeatured = product.IsFeatured,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                BrandId = product.BrandId,
                SubCategoryId = product.SubCategoryId,
                GenderId = product.GenderId,
                Variants = product.Variants
                    .Where(v =>
                        (!filter.ColourId.HasValue || v.ColourId == filter.ColourId.Value) &&
                        (!filter.SizeId.HasValue || v.SizeId == filter.SizeId.Value) &&
                        v.StockQuantity > 0)
                    .Select(ToDto) // Use the variant mapper
                    .ToList()
            };
        }



        public static ProductVariantDto ToDto(ProductVariant variant)
        {
            return new ProductVariantDto
            {
                Id = variant.Id,
                ProductId = variant.ProductId,
                SKU = variant.SKU,
                SizeId = variant.SizeId,
                Size = new Size
                {
                    Id = variant.Size.Id,
                    Name = variant.Size.Name
                },
                ColourId = variant.ColourId,
                Colour = new Colour
                {
                    Id = variant.Colour.Id,
                    Name = variant.Colour.Name
                },
                StockQuantity = variant.StockQuantity
            };
        }
    }
}