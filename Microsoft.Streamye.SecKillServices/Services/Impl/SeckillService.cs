using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Context;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Repositories;

namespace Microsoft.Streamye.SecKillServices.Services.Impl
{
    public class SeckillService : ISeckillService
    {
        public ISeckillRepository SeckillRepository;

        public SeckillService(ISeckillRepository SeckillRepository)
        {
            this.SeckillRepository = SeckillRepository;
        }

        public async Task CreateAsync(Seckill Seckill)
        {
            await SeckillRepository.CreateAsync(Seckill);
        }

        public async Task DeleteAsync(Seckill Seckill)
        {
            await SeckillRepository.DeleteAsync(Seckill);
        }

        public async Task<Seckill> GetSeckillByIdAsync(int id)
        {
            return await SeckillRepository.GetSeckillByIdAsync(id);
        }

        public async Task<IEnumerable<Seckill>> GetSeckillsAsync()
        {
            return await SeckillRepository.GetSeckillsAsync();
        }

        public async Task UpdateAsync(Seckill Seckill)
        {
            await SeckillRepository.UpdateAsync(Seckill);
        }

        public async Task<bool> SeckillExistsAsync(int id)
        {
            return await SeckillRepository.SeckillExistsAsync(id);
        }

        public async Task<IEnumerable<Seckill>> GetSeckillsAsync(Seckill seckill)
        {
            return await SeckillRepository.GetSeckillsAsync(seckill);
        }

        public async Task<Seckill> GetSeckillByProductIdAsync(int ProductId)
        {
            return await SeckillRepository.GetSeckillByProductIdAsync(ProductId);
        }
    }
}