using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.PayServices.Models;

namespace Microsoft.Streamye.PayServices.Context
{
    public class PayContext : DbContext
    {
        public PayContext(DbContextOptions<PayContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { set; get; }
    }
}