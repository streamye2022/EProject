namespace Microsoft.Streamye.OrderServices.Dtos.Input
{
    public class OrderCancelDto
    {
        public string OrderSn { set; get; } // 订单号
        public int OrderStatus { set; get; } // 订单状态
    }
}