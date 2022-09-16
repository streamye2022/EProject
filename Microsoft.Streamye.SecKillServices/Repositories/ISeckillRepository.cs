using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories
{
    public interface ISeckillRepository
    {
        Task<IEnumerable<Seckill>> GetSeckillsAsync();
        Task<IEnumerable<Seckill>> GetSeckillsAsync(Seckill seckill);
        Task<Seckill> GetSeckillByIdAsync(int id);
        Task<Seckill> GetSeckillByProductIdAsync(int ProductId);
        Task CreateAsync(Seckill Seckill);
        Task UpdateAsync(Seckill Seckill);
        Task DeleteAsync(Seckill Seckill);
        Task<bool> SeckillExistsAsync(int id);
    }
}