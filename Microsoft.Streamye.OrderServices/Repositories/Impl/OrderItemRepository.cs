using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Context;
using Microsoft.Streamye.OrderServices.Models;

namespace Microsoft.Streamye.OrderServices.Repositories.Impl
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public OrderContext OrderContext;

        public OrderItemRepository(OrderContext OrderContext)
        {
            this.OrderContext = OrderContext;
        }

        public async Task CreateAsync(OrderItem OrderItem)
        {
            await OrderContext.OrderItems.AddAsync(OrderItem);
            await OrderContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(OrderItem OrderItem)
        {
            OrderContext.OrderItems.Remove(OrderItem);
            await OrderContext.SaveChangesAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await OrderContext.OrderItems.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
        {
            return await OrderContext.OrderItems.ToListAsync();
        }

        public async Task UpdateAsync(OrderItem OrderItem)
        {
            OrderContext.OrderItems.Update(OrderItem);
            await OrderContext.SaveChangesAsync();
        }

        public async Task<bool> OrderItemExistsAsync(int id)
        {
            return await OrderContext.OrderItems.AnyAsync(e => e.Id == id);
        }
    }
}