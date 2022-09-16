using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Context;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories.Impl
{
    public class SeckillRecordRepository : ISeckillRecordRepository
    {
        public SeckillContext SeckillContext;

        public SeckillRecordRepository(SeckillContext SeckillContext)
        {
            this.SeckillContext = SeckillContext;
        }

        public async Task CreateAsync(SeckillRecord SeckillRecord)
        {
            await SeckillContext.SeckillRecords.AddAsync(SeckillRecord);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(SeckillRecord SeckillRecord)
        {
            SeckillContext.SeckillRecords.Remove(SeckillRecord);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<SeckillRecord> GetSeckillRecordByIdAsync(int id)
        {
            return await SeckillContext.SeckillRecords.FindAsync(id);
        }

        public async Task<IEnumerable<SeckillRecord>> GetSeckillRecordsAsync()
        {
            return await SeckillContext.SeckillRecords.ToListAsync();
        }

        public async Task UpdateAsync(SeckillRecord SeckillRecord)
        {
            SeckillContext.SeckillRecords.Update(SeckillRecord);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<bool> SeckillRecordExistsAsync(int id)
        {
            return await SeckillContext.SeckillRecords.AnyAsync(e => e.Id == id);
        }
    }
}