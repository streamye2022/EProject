using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Services;

namespace Microsoft.Streamye.SecKillServices.Controllers
{
    /// <summary>
    /// 秒杀时间服务控制器
    /// </summary>
    [Route("SeckillTime")]
    [ApiController]
    public class SeckillTimeController : ControllerBase
    {
        private readonly ISeckillTimeService SeckillTimeService;
        private readonly ISeckillService SeckillService;

        public SeckillTimeController(ISeckillTimeService SeckillTimeService,
            ISeckillService SeckillService)
        {
            this.SeckillTimeService = SeckillTimeService;
            this.SeckillService = SeckillService;
        }

        // GET: api/SeckillTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeckillTime>>> GetSeckillTimes()
        {
            var result = await SeckillTimeService.GetSeckillTimesAsync();
            return result.ToList();
        }

        // GET: api/SeckillTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeckillTime>> GetSeckillTime(int id)
        {
            var SeckillTime = await SeckillTimeService.GetSeckillTimeByIdAsync(id);

            if (SeckillTime == null)
            {
                return NotFound();
            }

            return SeckillTime;
        }

        /// <summary>
        /// 根据时间编号查询秒杀活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{timeId}/Seckills")]
        public async Task<ActionResult<IEnumerable<Seckill>>> GetSeckills(int timeId)
        {
            Seckill seckill = new Seckill();
            seckill.TimeId = timeId;
            var seckills = await SeckillService.GetSeckillsAsync(seckill);
            return seckills.ToList();
        }


        // PUT: api/SeckillTimes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeckillTime(int id, SeckillTime SeckillTime)
        {
            if (id != SeckillTime.Id)
            {
                return BadRequest();
            }

            try
            {
                await SeckillTimeService.UpdateAsync(SeckillTime);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SeckillTimeExists(id))
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

        // POST: api/SeckillTimes
        [HttpPost]
        public async Task<ActionResult<SeckillTime>> PostSeckillTime(SeckillTime SeckillTime)
        {
            SeckillTimeService.CreateAsync(SeckillTime);
            return CreatedAtAction("GetSeckillTime", new { id = SeckillTime.Id }, SeckillTime);
        }

        // DELETE: api/SeckillTimes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeckillTime>> DeleteSeckillTime(int id)
        {
            var SeckillTime = await SeckillTimeService.GetSeckillTimeByIdAsync(id);
            if (SeckillTime == null)
            {
                return NotFound();
            }

            await SeckillTimeService.DeleteAsync(SeckillTime);
            return SeckillTime;
        }

        private async Task<bool> SeckillTimeExists(int id)
        {
            return await SeckillTimeService.SeckillTimeExistsAsync(id);
        }
    }
}