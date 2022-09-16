using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.ProductServices.Models;

namespace Microsoft.Streamye.ProductServices.Services
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImage>> GetProductImagesAsync();
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(ProductImage productImage);
        Task<ProductImage> GetProductImageByIdAsync(int id);
        Task CreateAsync(ProductImage ProductImage);
        Task UpdateAsync(ProductImage ProductImage);
        Task DeleteAsync(ProductImage ProductImage);
        Task<bool> ProductImageExistsAsync(int id);
    }
}