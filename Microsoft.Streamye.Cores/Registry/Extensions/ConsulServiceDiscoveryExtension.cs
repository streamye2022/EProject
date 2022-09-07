using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.Registry.ConsulImpl;
using Microsoft.Streamye.Cores.Registry.Options;

namespace Microsoft.Streamye.Cores.Registry.Extensions
{
    public static class ConsulServiceDiscoveryExtension
    {

        public static IServiceCollection AddConsulServiceDiscovery(this IServiceCollection serviceCollection)
        {
            AddConsulServiceDiscovery(serviceCollection, option => { });
            return serviceCollection;
        }
        
        public static IServiceCollection AddConsulServiceDiscovery(this IServiceCollection serviceCollection, Action<ServiceDiscoveryOptions> options)
        {
            //inject options 1.Configure 2. 
            serviceCollection.Configure<ServiceDiscoveryOptions>(options);

            // 2: 直接new一个
            // ServiceDiscoveryOptions serviceDiscoveryOptions = new ServiceDiscoveryOptions();
            // options(serviceDiscoveryOptions);
            // serviceCollection.AddSingleton(serviceDiscoveryOptions);
            
            serviceCollection.AddSingleton<IServiceDiscovery,ConsulServiceDiscovery>();
            return serviceCollection;
        }
    }
}