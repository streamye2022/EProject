using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.OrderServices.Models;

namespace Microsoft.Streamye.OrderServices.Services
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task CreateAsync(OrderItem OrderItem);
        Task UpdateAsync(OrderItem OrderItem);
        Task DeleteAsync(OrderItem OrderItem);
        Task<bool> OrderItemExistsAsync(int id);
    }
}