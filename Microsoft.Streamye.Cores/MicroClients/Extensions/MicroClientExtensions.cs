using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.DynamicMiddleware;
using Microsoft.Streamye.Cores.DynamicMiddleware.Extensions;
using Microsoft.Streamye.Cores.MicroClients.Options;

namespace Microsoft.Streamye.Cores.MicroClients.Extensions
{
    public static class MicroClientExtensions
    {
        public static IServiceCollection AddMicroClients(this IServiceCollection serviceCollection)
        {
            AddMicroClients(serviceCollection, options => { });
            return serviceCollection;
        }

        public static IServiceCollection AddMicroClients(this IServiceCollection serviceCollection,
            Action<MicroClientOptions> options)
        {
            //构造option
            MicroClientOptions microClientOptions = new MicroClientOptions();
            options(microClientOptions);

            //添加dynamic middleware
            serviceCollection.AddDynamicMiddleware<IDynamicMiddlewareService, DefaultDynamicMiddlewareService>(microClientOptions.dynamicMiddlewareOption);
            
            //添加代理类
            serviceCollection.AddSingleton<MicroClientProxyFactory>();
            serviceCollection.AddSingleton<MicroClientList>();

            MicroClientList microClientList =
                serviceCollection.BuildServiceProvider().GetRequiredService<MicroClientList>();
            
            var clients= microClientList.GetClients(microClientOptions.AssemblyName);
            
            //拿到microClientList，将生成的Type和对应的object 都放入serviceCollection中
            foreach (var clientKey in clients.Keys)
            {
                serviceCollection.AddSingleton(clientKey,clients[clientKey]);
            }
            
            return serviceCollection;
        }
    }
}