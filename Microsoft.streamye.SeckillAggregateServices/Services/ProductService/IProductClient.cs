using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Models.ProductModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.ProductService
{
    [MicroClient("http", "ProductServices")]
    public interface IProductClient
    {
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products")]
        public Task<List<Product>> GetProductListAsync();


        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products/{productId}")]
        public Task<Product> GetProductAsync(int productId);

        /// <summary>
        /// 扣减商品库存
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCount"></param>
        /// <returns></returns>
        [PutPath("/Products/{ProductId}/set-stock")]
        public Task ProductSetStockAsync(int ProductId, int ProductCount);
    }
}