using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Context;
using Microsoft.Streamye.OrderServices.Models;

namespace Microsoft.Streamye.OrderServices.Repositories.Impl
{
    public class OrderRepository: IOrderRepository
    {
        public OrderContext OrderContext;
        public OrderRepository(OrderContext OrderContext)
        {
            this.OrderContext = OrderContext;
        }
        public async Task CreateAsync(Order Order)
        {
            await OrderContext.Orders.AddAsync(Order);
            await OrderContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order Order)
        {
            OrderContext.Orders.Remove(Order);
            await OrderContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await OrderContext.Orders.FindAsync(id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await OrderContext.Orders.ToListAsync();
        }

        public async Task UpdateAsync(Order Order)
        {
            OrderContext.Orders.Update(Order);
            await OrderContext.SaveChangesAsync();
        }
        public async Task<bool> OrderExistsAsync(int id)
        {
            return await OrderContext.Orders.AnyAsync(e => e.Id == id);
        }
    }
}