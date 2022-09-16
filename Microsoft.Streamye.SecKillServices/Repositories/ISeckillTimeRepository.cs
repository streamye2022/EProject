using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories
{
    public interface ISeckillTimeRepository
    {
        Task<IEnumerable<SeckillTime>> GetSeckillTimesAsync();
        Task<SeckillTime> GetSeckillTimeByIdAsync(int id);
        Task CreateAsync(SeckillTime SeckillTime);
        Task UpdateAsync(SeckillTime SeckillTime);
        Task DeleteAsync(SeckillTime SeckillTime);
        Task<bool> SeckillTimeExistsAsync(int id);
    }
}