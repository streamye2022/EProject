namespace Microsoft.Streamye.ProductServices.Dtos.Inputs
{
    /// <summary>
    /// 库存扣减Dto
    /// </summary>
    public class StockSetDto
    {
        // 商品编号
        public int ProductId { set; get; }
        // 商品数量
        public int ProductCount { set; get; }
    }
}
