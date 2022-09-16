using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.ProductServices.Models;
using Microsoft.Streamye.ProductServices.Repositories;

namespace Microsoft.Streamye.ProductServices.Services.Impl
{
    public class ProductImageService : IProductImageService
    {
        private IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = _productImageRepository;
        }

        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync()
        {
            return await _productImageRepository.GetProductImagesAsync();
        }

        //还可以优化参数
        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(ProductImage productImage)
        {
            return await _productImageRepository.GetProductImagesAsync(productImage);
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int id)
        {
            return await _productImageRepository.GetProductImageByIdAsync(id);
        }

        public async Task CreateAsync(ProductImage ProductImage)
        {
            await _productImageRepository.CreateAsync(ProductImage);
        }

        public async Task UpdateAsync(ProductImage ProductImage)
        {
            await _productImageRepository.UpdateAsync(ProductImage);
        }

        public async Task DeleteAsync(ProductImage ProductImage)
        {
            await _productImageRepository.DeleteAsync(ProductImage);
        }

        public async Task<bool> ProductImageExistsAsync(int id)
        {
            return await _productImageRepository.ProductImageExistsAsync(id);
        }
    }
}