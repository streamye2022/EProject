using System.Collections.Generic;

namespace Microsoft.Streamye.SeckillAggregateServices.Models.ProductModel
{
    public class Product
    {
        public int Id { set; get; }
        public string ProductCode { set; get; } //商品编码
        public string ProductUrl { set; get; } // 商品主图
        public string ProductTitle { set; get; } //商品标题
        public string ProductDescription { set; get; } // 商品描述
        public decimal ProductVirtualprice { set; get; } //商品虚拟价格
        public decimal ProductPrice { set; get; } //价格
        public int ProductSort { set; get; } //商品序号
        public int ProductSold { set; get; } //已售件数
        public int ProductStock { set; get; } //商品库存
        public string ProductStatus { set; get; } // 商品状态

        //商品图片
        public List<ProductImage> Images { set; get; }
    }
}