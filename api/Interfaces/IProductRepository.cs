using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Product;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync(ProductQueryObject productQueryObject);
        Task<RequestResult<Product>> GetProductByIdAsync(int id);
        Task<RequestResult<CreateProductDto>> AddProductAsync(CreateProductDto product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<ProductVariant> AddProductVariantAsync(ProductVariant variant);
        Task UpdateProductVariantAsync(ProductVariant variant);
        Task DeleteProductVariantAsync(int id);
        Task<bool> ProductExistsAsync(int id);
    }
}