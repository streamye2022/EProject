using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Context;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories.Impl
{
    public class SeckillTimeRepository : ISeckillTimeRepository
    {
        public SeckillContext SeckillContext;

        public SeckillTimeRepository(SeckillContext SeckillContext)
        {
            this.SeckillContext = SeckillContext;
        }

        public async Task CreateAsync(SeckillTime SeckillTime)
        {
            await SeckillContext.SeckillTimes.AddAsync(SeckillTime);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SeckillTime SeckillTime)
        {
            SeckillContext.SeckillTimes.Remove(SeckillTime);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<SeckillTime> GetSeckillTimeByIdAsync(int id)
        {
            return await SeckillContext.SeckillTimes.FindAsync(id);
        }

        public async Task<IEnumerable<SeckillTime>> GetSeckillTimesAsync()
        {
            return await SeckillContext.SeckillTimes.ToListAsync();
        }

        public async Task UpdateAsync(SeckillTime SeckillTime)
        {
            SeckillContext.SeckillTimes.Update(SeckillTime);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<bool> SeckillTimeExistsAsync(int id)
        {
            return await SeckillContext.SeckillTimes.AnyAsync(e => e.Id == id);
        }
    }
}