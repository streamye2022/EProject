using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories
{
    public interface ISeckillRecordRepository
    {
        Task<IEnumerable<SeckillRecord>> GetSeckillRecordsAsync();
        Task<SeckillRecord> GetSeckillRecordByIdAsync(int id);
        Task CreateAsync(SeckillRecord SeckillRecord);
        Task UpdateAsync(SeckillRecord SeckillRecord);
        Task DeleteAsync(SeckillRecord SeckillRecord);
        Task<bool> SeckillRecordExistsAsync(int id);
    }
}