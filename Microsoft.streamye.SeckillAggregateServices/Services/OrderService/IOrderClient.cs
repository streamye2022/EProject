using System.Threading.Tasks;
using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Dtos.OrderDto;
using Microsoft.Streamye.SeckillAggregateServices.Models.OrderModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.OrderService
{
    [MicroClient("http", "OrderServices")]
    public interface IOrderClient
    {
        [PostPath("/Orders")]
        public Task<Order> CreateOrderAsync(Order order);

        [PostPath("/Orders/Cancel")]
        public Task<Order> CancelOrderAsync(OrderCancelDto cancelDto);
    }
}