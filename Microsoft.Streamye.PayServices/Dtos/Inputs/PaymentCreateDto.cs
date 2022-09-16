namespace Microsoft.Streamye.PayServices.Dtos.Inputs
{
    public class PaymentCreateDto
    {
        public string ProductName { set; get; } // 商品名称
        public string OrderSn { set; get; } // 订单号
        public string OrderTotalPrice { set; get; } // 总价
        public string PaymentType { set; get; } // 支付类型
    }
}