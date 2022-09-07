using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Streamye.Cores.Registry
{
    public class ServiceRegistryIHostedService : IHostedService
    {
        private IServiceRegistry _serviceRegistry;

        public ServiceRegistryIHostedService(IServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _serviceRegistry.Register();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _serviceRegistry.DeRegister();
            return Task.CompletedTask;
        }
    }
}