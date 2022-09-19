using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.Commons.Exceptions;
using Microsoft.Streamye.SecKillServices.Dtos.Inputs;
using Microsoft.Streamye.SecKillServices.Models;
using Microsoft.Streamye.SecKillServices.Services;

namespace Microsoft.Streamye.SecKillServices.Controllers
{
    /// <summary>
    /// 秒杀服务控制器
    /// </summary>
    [Route("Seckills")]
    public class SeckkillController : ControllerBase
    {
        private readonly ISeckillService SeckillService;

        public SeckkillController(ISeckillService SeckillService)
        {
            this.SeckillService = SeckillService;
        }

        // GET: api/Seckills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seckill>>> GetSeckills()
        {
            var result = await SeckillService.GetSeckillsAsync();
            return result.ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="seckill"></param>
        /// <returns></returns>
        [HttpGet("GetList")]
        public async Task<ActionResult<IEnumerable<Seckill>>> GetList([FromQuery] Seckill seckill)
        {
            IEnumerable<Seckill> seckills = await SeckillService.GetSeckillsAsync(seckill);
            return seckills.ToList();
        }

        // GET: api/Seckills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seckill>> GetSeckill(int id)
        {
            var Seckill = await SeckillService.GetSeckillByIdAsync(id);

            if (Seckill == null)
            {
                return NotFound();
            }

            return Seckill;
        }

        // PUT: api/Seckills/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeckill(int id, Seckill Seckill)
        {
            if (id != Seckill.Id)
            {
                return BadRequest();
            }

            try
            {
                await SeckillService.UpdateAsync(Seckill);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await SeckillExists(id))
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
        /// 秒杀商品库存扣减
        /// </summary>
        /// <param name="seckillDto"></param>
        /// <returns></returns>
        [HttpPut("Stock/Subtract")]
        public async Task<IActionResult> PutSeckillProductStock(SeckillDto seckillDto)
        {
            // 1、查询秒杀库存
            Seckill seckill = await SeckillService.GetSeckillByProductIdAsync(seckillDto.ProductId);

            // 2、判断秒杀库存是否完成
            if (seckill.SeckillStock <= 0)
            {
                throw new BizException("秒杀库存完了");
            }

            // 3、扣减秒杀库存
            seckill.SeckillStock = seckill.SeckillStock - seckillDto.ProductCount;

            // 4、更新秒杀库存
            await SeckillService.UpdateAsync(seckill);

            return Ok("更新库存成功");
        }

        /// <summary>
        /// 异步扣减秒杀库存
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SeckillDto"></param>
        /// <returns></returns>
        [NonAction]
        [CapSubscribe("seckill.stock.subtract")]
        public async Task<IActionResult> SeckillProductStockSubtract(SeckillDto seckillPo)
        {
            // 1、查询秒杀库存
            Seckill seckill = await SeckillService.GetSeckillByProductIdAsync(seckillPo.ProductId);

            // 2、判断秒杀库存是否完成
            if (seckill.SeckillStock <= 0)
            {
                throw new BizException("秒杀库存完了");
            }

            // 3、扣减秒杀库存
            seckill.SeckillStock = seckill.SeckillStock - seckillPo.ProductCount;

            // 4、更新秒杀库存
            await SeckillService.UpdateAsync(seckill);
            return Ok("更新库存成功");
        }

        // 异步更新秒杀库存
        [NonAction]
        [CapSubscribe("seckill.stock.update")]
        public async Task<IActionResult> SeckillProductStock(SeckillStockUpdateDto seckillStockUpdateDto)
        {
            // 1、查询秒杀库存
            Seckill seckill = await SeckillService.GetSeckillByProductIdAsync(seckillStockUpdateDto.ProductId);

            // 4、更新秒杀库存
            seckill.SeckillStock = seckillStockUpdateDto.RemainStock;
            await SeckillService.UpdateAsync(seckill);
            return Ok("更新库存成功");
        }

        /// <summary>
        /// 秒杀商品库存恢复
        /// </summary>
        /// <param name="seckillPo"></param>
        /// <returns></returns>
        [HttpPut("Stock/Recover")]
        public async Task<IActionResult> SeckillProductStockRecover(SeckillRecoverDto seckillRecoverDto)
        {
            Seckill seckill = await SeckillService.GetSeckillByProductIdAsync(seckillRecoverDto.ProductId);

            seckill.SeckillStock = seckill.SeckillStock + seckillRecoverDto.SeckillNum;

            await SeckillService.UpdateAsync(seckill);
            return Ok("秒杀库存恢复成功");
        }


        // POST: api/Seckills
        [HttpPost]
        public async Task<ActionResult<Seckill>> PostSeckill(Seckill Seckill)
        {
            await SeckillService.CreateAsync(Seckill);
            return CreatedAtAction("GetSeckill", new { id = Seckill.Id }, Seckill);
        }

        // DELETE: api/Seckills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seckill>> DeleteSeckill(int id)
        {
            var Seckill = await SeckillService.GetSeckillByIdAsync(id);
            if (Seckill == null)
            {
                return NotFound();
            }

            await SeckillService.DeleteAsync(Seckill);
            return Seckill;
        }

        private async Task<bool> SeckillExists(int id)
        {
            return await SeckillService.SeckillExistsAsync(id);
        }
    }
}