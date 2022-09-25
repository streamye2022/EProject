using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.OrderServices.Models;

namespace Microsoft.Streamye.OrderServices.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { set; get; }
        public DbSet<OrderItem> OrderItems { set; get; }
    }
}