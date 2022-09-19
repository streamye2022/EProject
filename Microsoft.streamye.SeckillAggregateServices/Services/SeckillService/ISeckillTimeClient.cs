using System.Collections.Generic;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Models.SeckillModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.SeckillService
{
    [MicroClient("http", "SeckillServices")]
    public interface ISeckillTimeClient
    {
        /// <summary>
        /// 查询秒杀时间表
        /// </summary>
        /// <returns></returns>
        [GetPath("/SeckillTime")]
        public List<SeckillTime> GetSeckillTimes();

        /// <summary>
        /// 根据时间查询秒杀活动
        /// </summary>
        /// <returns></returns>
        [GetPath("/SeckillTime/{timeId}/Seckills")]
        public List<Seckill> GetSeckills([PathVariable("timeId")] int timeId);
    }
}