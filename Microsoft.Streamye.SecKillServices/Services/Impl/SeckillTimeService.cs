using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Context;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Repositories;

namespace Microsoft.Streamye.SecKillServices.Services.Impl
{
    public class SeckillTimeService : ISeckillTimeService
    {
        public ISeckillTimeRepository SeckillTimeRepository;

        public SeckillTimeService(ISeckillTimeRepository SeckillTimeRepository)
        {
            this.SeckillTimeRepository = SeckillTimeRepository;
        }

        public async Task CreateAsync(SeckillTime SeckillTime)
        {
            await SeckillTimeRepository.CreateAsync(SeckillTime);
        }

        public async Task DeleteAsync(SeckillTime SeckillTime)
        {
            await SeckillTimeRepository.DeleteAsync(SeckillTime);
        }

        public async Task<SeckillTime> GetSeckillTimeByIdAsync(int id)
        {
            return await SeckillTimeRepository.GetSeckillTimeByIdAsync(id);
        }

        public async Task<IEnumerable<SeckillTime>> GetSeckillTimesAsync()
        {
            return await SeckillTimeRepository.GetSeckillTimesAsync();
        }

        public async Task UpdateAsync(SeckillTime SeckillTime)
        {
            await SeckillTimeRepository.UpdateAsync(SeckillTime);
        }

        public async Task<bool> SeckillTimeExistsAsync(int id)
        {
            return await SeckillTimeRepository.SeckillTimeExistsAsync(id);
        }
    }
}