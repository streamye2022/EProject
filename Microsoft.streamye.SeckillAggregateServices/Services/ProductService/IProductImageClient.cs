using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Models.ProductModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.ProductService
{
    [MicroClient("http", "ProductServices")]
    public interface IProductImageClient
    {
        /// <summary>
        /// 查询所有商品images信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products/{productId}/ProductImages")]
        public Task<List<ProductImage>> GetProductImagesAsync(int productId);
    }
}