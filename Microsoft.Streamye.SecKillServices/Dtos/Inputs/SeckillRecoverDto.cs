namespace Microsoft.Streamye.SecKillServices.Dtos.Inputs
{
    /// <summary>
    /// 秒杀库存恢复Dto
    /// </summary>
    public class SeckillRecoverDto
    {
        // 商品编号
        public int ProductId { set; get; }

        // 秒杀库存数量
        public int SeckillNum { set; get; }
    }
}