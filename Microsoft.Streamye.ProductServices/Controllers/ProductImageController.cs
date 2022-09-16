using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.ProductServices.Models;
using Microsoft.Streamye.ProductServices.Services;

namespace Microsoft.Streamye.ProductServices.Controllers
{
    [Route("Products/{productId}/ProductImages")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImages(int ProductId)
        {
            ProductImage productImage = new ProductImage();
            productImage.ProductId = ProductId;
            var results = await productImageService.GetProductImagesAsync(productImage);
            return results.ToList();
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
            var productImage = await productImageService.GetProductImageByIdAsync(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage;
        }

        // PUT: api/ProductImages/5
        // PUT 是完整的修改，patch是修改部分字段
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.Id)
            {
                return BadRequest();
            }

            try
            {
                await productImageService.UpdateAsync(productImage);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductImages
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostProductImage(ProductImage productImage)
        {
            try
            {
                productImageService.CreateAsync(productImage);
            }
            catch (DbUpdateException)
            {
                if (await ProductImageExists(productImage.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductImage", new { id = productImage.Id }, productImage);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductImage>> DeleteProductImage(int id)
        {
            var productImage = await productImageService.GetProductImageByIdAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            await productImageService.DeleteAsync(productImage);
            return productImage;
        }

        private async Task<bool> ProductImageExists(int id)
        {
            return await productImageService.ProductImageExistsAsync(id);
        }
    }
}