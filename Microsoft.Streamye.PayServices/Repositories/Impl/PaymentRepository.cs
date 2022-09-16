using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.PayServices.Context;
using Microsoft.Streamye.PayServices.Models;

namespace Microsoft.Streamye.PayServices.Repositories.Impl
{
    public class PaymentRepository : IPaymentRepository
    {
        public PayContext PaymentContext;
        public PaymentRepository(PayContext PaymentContext)
        {
            this.PaymentContext = PaymentContext;
        }
        public async Task CreateAsync(Payment Payment)
        {
            PaymentContext.Payments.Add(Payment);
            await PaymentContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Payment Payment)
        {
            PaymentContext.Payments.Remove(Payment);
            await PaymentContext.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await PaymentContext.Payments.FindAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await PaymentContext.Payments.ToListAsync();
        }

        public async Task UpdateAsync(Payment Payment)
        {
            PaymentContext.Payments.Update(Payment);
            await PaymentContext.SaveChangesAsync();
        }
        public async Task<bool> PaymentExistsAsync(int id)
        {
            return await PaymentContext.Payments.AnyAsync(e => e.Id == id);
        }
    }
}