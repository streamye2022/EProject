using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.PayServices.Dtos.Inputs;
using Microsoft.Streamye.PayServices.Models;
using Microsoft.Streamye.PayServices.Services;


namespace Microsoft.Streamye.PayServices.Controllers
{
    /// <summary>
    /// 支付服务控制器
    /// </summary>
    [Route("Payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService PaymentService;

        // 微信支付客户端, 支付宝网页支付客户端 => 支付工厂 => 支付facade
        private WebPagePayFacade _webPagePayFacade; // 统一支付外观

        public PaymentController(IPaymentService PaymentService,
            WebPagePayFacade webPagePayFacade)
        {
            this.PaymentService = PaymentService;
            _webPagePayFacade = webPagePayFacade;
        }


        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var result = await PaymentService.GetPaymentsAsync();
            return result.ToList();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var Payment = await PaymentService.GetPaymentByIdAsync(id);

            if (Payment == null)
            {
                return NotFound();
            }

            return Payment;
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment Payment)
        {
            if (id != Payment.Id)
            {
                return BadRequest();
            }

            try
            {
                await PaymentService.UpdateAsync(Payment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        /// <summary>
        /// 支付宝，微信组合
        /// </summary>
        /// <param name="paymentCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<WebPagePayResult>> PostCombinePayment(PaymentCreateDto paymentCreateDto)
        {
            // 外观模式选择支付
            WebPagePayResult webPagePayResult = _webPagePayFacade.CreatePay
            (paymentCreateDto.PaymentType,
                paymentCreateDto.ProductName,
                paymentCreateDto.OrderSn,
                paymentCreateDto.OrderTotalPrice);

            return webPagePayResult;
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePayment(int id)
        {
            var Payment = await PaymentService.GetPaymentByIdAsync(id);
            if (Payment == null)
            {
                return NotFound();
            }

            await PaymentService.DeleteAsync(Payment);
            return Payment;
        }

        private async Task<bool> PaymentExists(int id)
        {
            return await PaymentService.PaymentExistsAsync(id);
        }
    }
}