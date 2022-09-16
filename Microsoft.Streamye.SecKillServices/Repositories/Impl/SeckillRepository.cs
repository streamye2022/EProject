using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Context;
using Microsoft.Streamye.SecKillServices.Models;

namespace Microsoft.Streamye.SecKillServices.Repositories.Impl
{
    public class SeckillRepository : ISeckillRepository
    {
        public SeckillContext SeckillContext;

        public SeckillRepository(SeckillContext SeckillContext)
        {
            this.SeckillContext = SeckillContext;
        }

        public async Task CreateAsync(Seckill Seckill)
        {
            await SeckillContext.Seckills.AddAsync(Seckill);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Seckill Seckill)
        {
            SeckillContext.Seckills.Remove(Seckill);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<Seckill> GetSeckillByIdAsync(int id)
        {
            return await SeckillContext.Seckills.FindAsync(id);
        }

        public async Task<IEnumerable<Seckill>> GetSeckillsAsync()
        {
            return await SeckillContext.Seckills.ToListAsync();
        }

        public async Task UpdateAsync(Seckill Seckill)
        {
            SeckillContext.Seckills.Update(Seckill);
            await SeckillContext.SaveChangesAsync();
        }

        public async Task<bool> SeckillExistsAsync(int id)
        {
            return await SeckillContext.Seckills.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Seckill>> GetSeckillsAsync(Seckill seckill)
        {
            IQueryable<Seckill> query = SeckillContext.Seckills;
            if (seckill.Id != 0)
            {
                query = query.Where(s => s.Id == seckill.Id);
            }

            if (seckill.SeckillType != 0)
            {
                query = query.Where(s => s.SeckillType == seckill.SeckillType);
            }

            if (seckill.SeckillName != null)
            {
                query = query.Where(s => s.SeckillName == seckill.SeckillName);
            }

            if (seckill.SeckillUrl != null)
            {
                query = query.Where(s => s.SeckillUrl == seckill.SeckillUrl);
            }

            if (seckill.SeckillPrice != 0)
            {
                query = query.Where(s => s.SeckillPrice == seckill.SeckillPrice);
            }

            if (seckill.SeckillStock != 0)
            {
                query = query.Where(s => s.SeckillStock == seckill.SeckillStock);
            }

            if (seckill.SeckillPercent != null)
            {
                query = query.Where(s => s.SeckillPercent == seckill.SeckillPercent);
            }

            if (seckill.TimeId != 0)
            {
                query = query.Where(s => s.TimeId == seckill.TimeId);
            }

            if (seckill.ProductId != 0)
            {
                query = query.Where(s => s.ProductId == seckill.ProductId);
            }

            if (seckill.SeckillLimit != 0)
            {
                query = query.Where(s => s.SeckillLimit == seckill.SeckillLimit);
            }

            if (seckill.SeckillDescription != null)
            {
                query = query.Where(s => s.SeckillDescription == seckill.SeckillDescription);
            }

            if (seckill.SeckillIstop != 0)
            {
                query = query.Where(s => s.SeckillIstop == seckill.SeckillIstop);
            }

            if (seckill.SeckillStatus != 0)
            {
                query = query.Where(s => s.SeckillStatus == seckill.SeckillStatus);
            }

            return await query.ToListAsync();
        }

        public async Task<Seckill> GetSeckillByProductIdAsync(int ProductId)
        {
            List<Seckill> seckills = await SeckillContext.Seckills.Where(s => s.ProductId == ProductId).ToListAsync();
            return seckills.Count > 0 ? seckills[0] : null;
        }
    }
}