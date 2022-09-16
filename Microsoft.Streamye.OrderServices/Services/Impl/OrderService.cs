using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Context;
using Microsoft.Streamye.OrderServices.Models;
using Microsoft.Streamye.OrderServices.Repositories;

namespace Microsoft.Streamye.OrderServices.Services.Impl
{
    public class OrderService : IOrderService
    {
        public IOrderRepository OrderRepository;

        public OrderService(IOrderRepository OrderRepository)
        {
            this.OrderRepository = OrderRepository;
        }

        public async Task CreateAsync(Order Order)
        {
            await OrderRepository.CreateAsync(Order);
        }

        public async Task DeleteAsync(Order Order)
        {
            await OrderRepository.DeleteAsync(Order);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await OrderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await OrderRepository.GetOrdersAsync();
        }

        public async Task UpdateAsync(Order Order)
        {
            await OrderRepository.UpdateAsync(Order);
        }

        public async Task<bool> OrderExistsAsync(int id)
        {
            return await OrderRepository.OrderExistsAsync(id);
        }
    }
}