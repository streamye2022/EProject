using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.ProductServices.Models;

namespace Microsoft.Streamye.ProductServices.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task CreateAsync(Product Product);
        Task UpdateAsync(Product Product);
        Task DeleteAsync(Product Product);
        Task<bool> ProductExistsAsync(int id);
    }
}