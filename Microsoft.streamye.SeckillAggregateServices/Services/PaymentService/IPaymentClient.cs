using System.Threading.Tasks;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Dtos.PaymentDto;
using Microsoft.Streamye.SeckillAggregateServices.Models.PaymentModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.PaymentService
{
    [MicroClient("http", "PaymentServices")]
    public interface IPaymentClient
    {
        [PostPath("/Payments")]
        public Task<PaymentResultDto> PayAsync(PaymentCreateDto paymentCreateDto);
    }
}