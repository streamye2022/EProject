using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.PayServices.Models;
using Microsoft.Streamye.PayServices.Repositories;

namespace Microsoft.Streamye.PayServices.Services.Impl
{
    public class PaymentService : IPaymentService
    {
        public IPaymentRepository PaymentRepository;

        public PaymentService(IPaymentRepository PaymentRepository)
        {
            this.PaymentRepository = PaymentRepository;
        }

        public async Task CreateAsync(Payment Payment)
        {
            await PaymentRepository.CreateAsync(Payment);
        }

        public async Task DeleteAsync(Payment Payment)
        {
            await PaymentRepository.DeleteAsync(Payment);
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await PaymentRepository.GetPaymentByIdAsync(id);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await PaymentRepository.GetPaymentsAsync();
        }

        public async Task UpdateAsync(Payment Payment)
        {
            await PaymentRepository.UpdateAsync(Payment);
        }

        public async Task<bool> PaymentExistsAsync(int id)
        {
            return await PaymentRepository.PaymentExistsAsync(id);
        }
    }
}