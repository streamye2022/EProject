namespace Microsoft.Streamye.SeckillAggregateServices.Models.ProductModel
{
    public class ProductImage
    {
        public int Id { set; get; } // 编号
        public int ProductId { set; get; } // 商品编号
        public int ImageSort { set; get; } // 排序
        public string ImageStatus { set; get; } // 状态（1：启用，2：禁用）
        public string ImageUrl { set; get; } // 图片url
    }
}