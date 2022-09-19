using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Services;

namespace Microsoft.Streamye.SecKillServices.Controllers
{
    [Route("Seckills/{SeckillId}/SeckillRecords")]
    [ApiController]
    public class SeckillRecordController : ControllerBase
    {
        private readonly ISeckillRecordService SeckillRecordService;
        private readonly ISeckillService SeckillService;


        public SeckillRecordController(ISeckillRecordService SeckillRecordService,
            ISeckillService SeckillService)
        {
            this.SeckillRecordService = SeckillRecordService;
            this.SeckillService = SeckillService;
        }

        // GET: api/SeckillRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeckillRecord>>> GetSeckillRecords(int SeckillId)
        {
            var result = await SeckillRecordService.GetSeckillRecordsAsync();

            return result.ToList();
        }

        // GET: api/SeckillRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeckillRecord>> GetSeckillRecord(int id)
        {
            var SeckillRecord = await SeckillRecordService.GetSeckillRecordByIdAsync(id);

            if (SeckillRecord == null)
            {
                return NotFound();
            }

            return SeckillRecord;
        }

        // PUT: api/SeckillRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeckillRecord(int id, SeckillRecord SeckillRecord)
        {
            if (id != SeckillRecord.Id)
            {
                return BadRequest();
            }

            try
            {
                await SeckillRecordService.UpdateAsync(SeckillRecord);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SeckillRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 添加秒杀记录
        /// </summary>
        /// <param name="SeckillRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<SeckillRecord>> PostSeckillRecord(SeckillRecord SeckillRecord)
        {
            await SeckillRecordService.CreateAsync(SeckillRecord);
            if (SeckillRecord.RecordStatus == 1)
            {
                // 1、查询秒杀库存
                Seckill seckill = await SeckillService.GetSeckillByIdAsync(SeckillRecord.SeckillId);

                // 2、增加秒杀库存 ??? 减去？？
                seckill.SeckillStock = seckill.SeckillStock + SeckillRecord.SeckillNum;

                // 3、更新秒杀库存
                await SeckillService.UpdateAsync(seckill);
            }

            return CreatedAtAction("GetSeckillRecord", new { id = SeckillRecord.Id }, SeckillRecord);
        }

        // DELETE: api/SeckillRecords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeckillRecord>> DeleteSeckillRecord(int id)
        {
            var SeckillRecord = await SeckillRecordService.GetSeckillRecordByIdAsync(id);
            if (SeckillRecord == null)
            {
                return NotFound();
            }

            await SeckillRecordService.DeleteAsync(SeckillRecord);
            return SeckillRecord;
        }

        private async Task<bool> SeckillRecordExists(int id)
        {
            return await SeckillRecordService.SeckillRecordExistsAsync(id);
        }
    }
}