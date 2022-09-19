using Microsoft.Streamye.Cores.MicroClients.Attributes;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.SeckillService
{
    [MicroClient("http", "SeckillServices")]
    public interface ISeckillRecordClient
    {
        /// <summary>
        /// 查询秒杀活动列表
        /// </summary>
        /// <returns></returns>
        [PostPath("Seckills/{SeckillId}/SeckillRecords")]
        public void InsertSeckillRecord(int SeckillId, int SeckillNum, int UserId, int RecordStatus);
    }
}