using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.ProductServices.Context;
using Microsoft.Streamye.ProductServices.Models;

namespace Microsoft.Streamye.ProductServices.Repositories.Impl
{
    public class ProductRepository : IProductRepository
    {
        private ProductContext ProductContext;

        public ProductRepository(ProductContext productContext)
        {
            ProductContext = productContext;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await ProductContext.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await ProductContext.Products.FindAsync(id);
        }

        public async Task CreateAsync(Product Product)
        {
            ProductContext.Products.Add(Product);
            await ProductContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product Product)
        {
            ProductContext.Products.Update(Product);
            await ProductContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product Product)
        {
            ProductContext.Products.Remove(Product);
            await ProductContext.SaveChangesAsync();
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await ProductContext.Products.AnyAsync(e => e.Id == id);
        }
    }
}