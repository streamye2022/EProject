﻿namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.OrderDto
{
    /// <summary>
    /// 订单表单(接受参数)
    /// </summary>
    public class OrderCreateDto
    {
        public int ProductId { set; get; } // 商品编号
        public string ProductName { set; get; } // 商品名称
        public int ProductCount { set; get; }// 商品数量
        public decimal OrderTotalPrice { set; get; } // 订单价格
        public string ProductUrl { set; get; } // 商品图片
        public int UserId { set; get; } // 用户Id
        public string RequestId { set; get; }//页面Id
    }
}
