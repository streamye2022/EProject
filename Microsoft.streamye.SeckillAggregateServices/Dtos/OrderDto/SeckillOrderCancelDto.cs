using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.OrderDto
{
    /// <summary>
    /// 秒杀订单取消Dto
    /// </summary>
    public class SeckillOrderCancelDto
    {
        public string OrderSn { set; get; } // 订单号

        public int ProductId { set; get; } // 商品编号

        public int SeckillNum { set; get; } // 秒杀库存数量
    }
}