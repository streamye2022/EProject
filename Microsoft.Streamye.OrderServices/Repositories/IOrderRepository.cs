using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.OrderServices.Models;

namespace Microsoft.Streamye.OrderServices.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task CreateAsync(Order Order);
        Task UpdateAsync(Order Order);
        Task DeleteAsync(Order Order);
        Task<bool> OrderExistsAsync(int id);
    }
}