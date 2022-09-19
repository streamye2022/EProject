namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.PaymentDto
{
    //在支付回调的时候
    public class PaymentCallBackDto
    {
        public int OrderId { set; get; } // 订单主键
        public string OrderSn { set; get; } // 订单号
        public decimal OrderTotalPrice { set; get; } // 订单金额
        public int UserId { set; get; } // 用户编号
        public int ProductId { set; get; } // 商品编号
        public string ProductName { set; get; } // 商品名称
    }
}