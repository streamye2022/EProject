using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.ProductServices.Models;
using Microsoft.Streamye.ProductServices.Repositories;

namespace Microsoft.Streamye.ProductServices.Services.Impl
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository ProductRepository;

        public ProductService(IProductRepository ProductRepository)
        {
            this.ProductRepository = ProductRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await ProductRepository.GetProductsAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await ProductRepository.GetProductByIdAsync(id);
        }

        public async Task CreateAsync(Product Product)
        {
            await ProductRepository.CreateAsync(Product);
        }

        public async Task UpdateAsync(Product Product)
        {
            await ProductRepository.UpdateAsync(Product);
        }

        public async Task DeleteAsync(Product Product)
        {
            await ProductRepository.DeleteAsync(Product);
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await ProductRepository.ProductExistsAsync(id);
        }
    }
}