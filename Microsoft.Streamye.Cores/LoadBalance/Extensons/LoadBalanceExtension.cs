using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.LoadBalance.Options;

namespace Microsoft.Streamye.Cores.LoadBalance.Extensons
{
    public static class LoadBalanceExtension
    {
        public static IServiceCollection AddRandomLoadBalance(this IServiceCollection services)
        {
            AddRandomLoadBalance(services, options => { });
            return services;
        }
        
        //add other load balance
        public static IServiceCollection AddRandomLoadBalance(this IServiceCollection serviceCollection, Action<LoadBalanceOptions> options)
        {
            // LoadBalanceOptions balanceOptions = new LoadBalanceOptions();
            // options(balanceOptions);
            serviceCollection.Configure(options);

            //
            serviceCollection.AddSingleton<ILoadBalance,RandomLoadBalance>();
            return serviceCollection;
        }
    }
}