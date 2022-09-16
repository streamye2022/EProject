using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Context;
using Microsoft.Streamye.OrderServices.Models;
using Microsoft.Streamye.OrderServices.Repositories;

namespace Microsoft.Streamye.OrderServices.Services.Impl
{
    public class OrderItemService
    {
        public IOrderItemRepository OrderItemRepository;

        public OrderItemService(IOrderItemRepository OrderItemRepository)
        {
            this.OrderItemRepository = OrderItemRepository;
        }

        public async Task CreateAsync(OrderItem OrderItem)
        {
            await OrderItemRepository.CreateAsync(OrderItem);
        }

        public async Task DeleteAsync(OrderItem OrderItem)
        {
            await OrderItemRepository.DeleteAsync(OrderItem);
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await OrderItemRepository.GetOrderItemByIdAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
        {
            return await OrderItemRepository.GetOrderItemsAsync();
        }

        public async Task UpdateAsync(OrderItem OrderItem)
        {
            await OrderItemRepository.UpdateAsync(OrderItem);
        }

        public async Task<bool> OrderItemExistsAsync(int id)
        {
            return await OrderItemRepository.OrderItemExistsAsync(id);
        }
    }
}