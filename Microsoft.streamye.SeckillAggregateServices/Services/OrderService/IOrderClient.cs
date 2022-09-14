using System.Threading.Tasks;
using Microsoft.Streamye.SeckillAggregateServices.Dtos.OrderDto;
using Microsoft.Streamye.SeckillAggregateServices.Models.OrderModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.OrderService
{
    public interface IOrderClient
    {
        public Task<Order> CreateOrderAsync(Order order);

        public Task<Order> CancelOrderAsync(OrderCancelDto cancelDto);
    }
}