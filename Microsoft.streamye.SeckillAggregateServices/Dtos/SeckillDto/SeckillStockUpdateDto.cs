namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.SeckillDto
{
    public class SeckillStockUpdateDto
    {
        public int ProductId { set; get; }
        public int ProductCount { set; get; }
        public int RemainStock { set; get; } // 剩余库存
        public int UserId { set; get; } // 用户Id
    }
}