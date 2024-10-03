using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(ProductQueryObject productQueryObject);
        Task<Product> GetProductByIdAsync(string sku);
        Task<Product> AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<ProductVariant> AddProductVariantAsync(ProductVariant variant);
        Task UpdateProductVariantAsync(ProductVariant variant);
        Task DeleteProductVariantAsync(int id);
    }
}