namespace Microsoft.Streamye.SecKillServices.Models.SeckillService
{
    public class SeckillTime
    {
        public int Id { set; get; } // 秒杀时间编号
        public string TimeTitleUrl { set; get; } // 秒杀时间主题url
        public string SeckillDate { set; get; } // 秒杀日期
        public string SeckillStarttime { set; get; } // 秒杀开始时间点
        public string SeckillEndtime { set; get; } // 秒杀结束时间点
        public int TimeStatus { set; get; } // 秒杀时间状态（0：启动，1：禁用）
    }
}