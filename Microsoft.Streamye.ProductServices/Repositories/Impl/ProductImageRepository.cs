using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.ProductServices.Context;
using Microsoft.Streamye.ProductServices.Models;

namespace Microsoft.Streamye.ProductServices.Repositories.Impl
{
    public class ProductImageRepository : IProductImageRepository
    {
        private ProductContext productContext;

        public ProductImageRepository(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync()
        {
            return await productContext.ProductImages.ToListAsync();
        }

        //还可以优化参数
        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(ProductImage productImage)
        {
            return await productContext.ProductImages
                .Where(e => e.ProductId == productImage.ProductId)
                .Where(e => e.ImageSort == productImage.ImageSort)
                .Where(e => e.ImageUrl == productImage.ImageUrl)
                // .Where(e => e.Id == productImage.Id)
                .ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            return await productContext.ProductImages.FindAsync(id);
        }

        public async Task CreateAsync(ProductImage ProductImage)
        {
            productContext.ProductImages.Add(ProductImage);
            await productContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductImage ProductImage)
        {
            productContext.ProductImages.Update(ProductImage);
            await productContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductImage ProductImage)
        {
            productContext.ProductImages.Remove(ProductImage);
            await productContext.SaveChangesAsync();
        }

        public async Task<bool> ProductImageExistsAsync(int id)
        {
            return await productContext.ProductImages.AnyAsync(e => e.Id == id);
        }
    }
}