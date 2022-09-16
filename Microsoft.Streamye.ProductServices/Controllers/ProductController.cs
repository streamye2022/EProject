using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.ProductServices.Dtos.Inputs;
using Microsoft.Streamye.ProductServices.Models;
using Microsoft.Streamye.ProductServices.Services;

namespace Microsoft.Streamye.ProductServices.Controllers
{
    [Route("Products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var result = await productService.GetProductsAsync();
            return result.ToList();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // 1、CommonResult
            return product;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
                productService.UpdateAsync(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Accepted();
        }
        
        [HttpPut("{id}/set-stock")]
        public async Task<IActionResult> PutProductStock(int id, StockSetDto productVo)
        {
            if (id != productVo.ProductId)
            {
                return BadRequest();

            }
            // 1、查询商品
            Product product = await productService.GetProductByIdAsync(productVo.ProductId);

            // 2、判断商品库存是否完成
            if (product.ProductStock <= 0)
            {
                throw new BizException("库存完了");
            }

            // 3、扣减商品库存
            product.ProductStock = product.ProductStock - productVo.ProductCount;

            // 4、更新商品库存
            await productService.UpdateAsync(product);

            return Ok("更新库存成功");
        }
        
        
        private async Task<bool> ProductExists(int id)
        {
            return await productService.ProductExistsAsync(id);
        }
    }
}