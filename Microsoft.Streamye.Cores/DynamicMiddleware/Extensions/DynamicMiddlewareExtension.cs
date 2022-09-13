using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.DynamicMiddleware.options;
using Microsoft.Streamye.Cores.DynamicMiddleware.Urls;
using Microsoft.Streamye.Cores.LoadBalance.Extensons;
using Microsoft.Streamye.Cores.Middleware.Extensions;
using Microsoft.Streamye.Cores.Registry.Extensions;

namespace Microsoft.Streamye.Cores.DynamicMiddleware.Extensions
{
    public static class DynamicMiddlewareExtension
    {
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(
            this IServiceCollection serviceCollection)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            return AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(serviceCollection, options => { });
        }

        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(
            this IServiceCollection serviceCollection, Action<DynamicMiddlewareOption> option)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            DynamicMiddlewareOption dynamicMiddlewareOption = new DynamicMiddlewareOption();
            option(dynamicMiddlewareOption);

            //注册服务发现
            serviceCollection.AddConsulServiceDiscovery(dynamicMiddlewareOption.serviceDiscoveryOptions);
            //负载均衡
            serviceCollection.AddRandomLoadBalance(dynamicMiddlewareOption.loadBalanceOptions);
            //注册middleware service
            serviceCollection.AddMiddlewareServices(dynamicMiddlewareOption.middlewareOptions);

            //注册URL
            serviceCollection.AddSingleton<IDynamicUrl, DefaultDynamicUrl>();

            //注册动态服务
            serviceCollection.AddSingleton<TMiddleService, TMiddleImplementation>();
            return serviceCollection;
        }
    }
}