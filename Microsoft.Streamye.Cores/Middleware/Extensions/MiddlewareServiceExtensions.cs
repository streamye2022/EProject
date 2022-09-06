

using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.Middleware.Options;
using Microsoft.Streamye.Cores.Pollys.Extensions;

namespace Microsoft.Streamye.Cores.Middleware.Extensions
{
    public static class MiddlewareServiceExtensions
    {
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection serviceCollection)
        {
            AddMiddlewareServices(serviceCollection, options => { });
            return serviceCollection;
        }
        
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection serviceCollection, Action<MiddlewareOptions> options)
        {
            MiddlewareOptions middlewareOptions = new MiddlewareOptions();
            options(middlewareOptions); // 新建一个options, 然后给这个options赋值
            
            serviceCollection.Configure<MiddlewareOptions>(options);
            
            // serviceCollection.AddHttpClient(middlewareOptions.HttpClientName);
            serviceCollection.AddPollyHttpClient(middlewareOptions.HttpClientName, middlewareOptions.pollyHttpClientOptions);
            
            serviceCollection.AddSingleton<IMiddleService, HttpMiddleService>();

            return serviceCollection;
        }

    }
}