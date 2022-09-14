using System.Collections.Generic;
using Microsoft.Streamye.SeckillAggregateServices.Models.SeckillModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services
{
    public class SeckillServiceConvent
    {
        // 将 dto 转化成 dicts
        public static IDictionary<string, object> ConventToDicts(Seckill seckill)
        {
            return new Dictionary<string, object>();
        }
    }
}