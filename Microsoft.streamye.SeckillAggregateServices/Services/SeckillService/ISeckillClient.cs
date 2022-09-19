using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Models.SeckillModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.SeckillService
{
    [MicroClient("http", "SeckillServices")]
    public interface ISeckillClient
    {
        /// <summary>
        /// 查询秒杀活动集合
        /// </summary>
        /// <returns></returns>
        [GetPath("/Seckills")]
        public Task<List<Seckill>> GetSeckillsAsync();

        /// <summary>
        /// 根据秒杀Id查询秒杀活动
        /// </summary>
        /// <param name="seckillId"></param>
        /// <returns></returns>
        [GetPath("/Seckills/{seckillId}")]
        public Task<Seckill> GetSeckillAsync(int seckillId);

        /// <summary>
        /// 查询秒杀活动，通过时间条件查询
        /// </summary>
        /// <returns></returns>
        [GetPath("/Seckills/GetList")]
        public Task<List<Seckill>> GetSeckillsByTimeIdAsync(string TimeId);

        // <summary>
        /// 扣减秒杀商品库存
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCount"></param>
        /// <returns></returns>
        [PutPath("/Seckills/Stock/Subtract")]
        public Task SubtractSeckillStockAsync(int ProductId, int ProductCount);

        // <summary>
        /// 扣减秒杀商品库存
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCount"></param>
        /// <returns></returns>
        [PutPath("/Seckills/Stock/Recover")]
        public Task RecoverSeckillStockAsync(int ProductId, int SeckillNum);
    }
}