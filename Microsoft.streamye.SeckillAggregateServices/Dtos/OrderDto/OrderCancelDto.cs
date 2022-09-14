using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.OrderDto
{
    /// <summary>
    /// 订单取消Dto
    /// </summary>
    public class OrderCancelDto
    {
        public string OrderSn { set; get; } // 订单号
        public int OrderStatus { set; get; } // 订单状态
    }
}