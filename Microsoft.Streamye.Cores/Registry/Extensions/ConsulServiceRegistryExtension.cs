using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.Registry.ConsulImpl;
using Microsoft.Streamye.Cores.Registry.Options;

namespace Microsoft.Streamye.Cores.Registry.Extensions
{
    public static class ConsulServiceRegistryExtension
    {
        public static IServiceCollection AddConsulServiceRegistry(this IServiceCollection serviceCollection)
        {
            return AddConsulServiceRegistry(serviceCollection, options => { });
        }

        public static IServiceCollection AddConsulServiceRegistry(this IServiceCollection serviceCollection,
            Action<ServiceRegisterOptions> options)
        {
            // inject options
            serviceCollection.Configure<ServiceRegisterOptions>(options);

            // inject registry service
            serviceCollection.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();

            // inject IHostService
            serviceCollection.AddHostedService<ServiceRegistryIHostedService>();
            
            return serviceCollection;
        }
    }
}