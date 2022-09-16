using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.PayServices.Models;

namespace Microsoft.Streamye.PayServices.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(int id);
        Task CreateAsync(Payment Payment);
        Task UpdateAsync(Payment Payment);
        Task DeleteAsync(Payment Payment);
        Task<bool> PaymentExistsAsync(int id);
    }
}