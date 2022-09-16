using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Repositories;

namespace Microsoft.Streamye.SecKillServices.Services.Impl
{
    public class SeckillRecordService : ISeckillRecordService
    {
        public ISeckillRecordRepository SeckillRecordRepository;

        public SeckillRecordService(ISeckillRecordRepository SeckillRecordRepository)
        {
            this.SeckillRecordRepository = SeckillRecordRepository;
        }

        public async Task CreateAsync(SeckillRecord SeckillRecord)
        {
            await SeckillRecordRepository.CreateAsync(SeckillRecord);
        }

        public async Task DeleteAsync(SeckillRecord SeckillRecord)
        {
            await SeckillRecordRepository.DeleteAsync(SeckillRecord);
        }

        public async Task<SeckillRecord> GetSeckillRecordByIdAsync(int id)
        {
            return await SeckillRecordRepository.GetSeckillRecordByIdAsync(id);
        }

        public async Task<IEnumerable<SeckillRecord>> GetSeckillRecordsAsync()
        {
            return await SeckillRecordRepository.GetSeckillRecordsAsync();
        }

        public async Task UpdateAsync(SeckillRecord SeckillRecord)
        {
            await SeckillRecordRepository.UpdateAsync(SeckillRecord);
        }

        public async Task<bool> SeckillRecordExistsAsync(int id)
        {
            return await SeckillRecordRepository.SeckillRecordExistsAsync(id);
        }
    }
}