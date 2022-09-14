using System;
using Castle.DynamicProxy;
using Microsoft.Streamye.Cores.DynamicMiddleware;

namespace Microsoft.Streamye.Cores.MicroClients
{
    public class MicroClientProxyFactory
    {
        private IDynamicMiddlewareService _dynamicMiddlewareService;

        //注入dynamic middle ware service
        public MicroClientProxyFactory(IDynamicMiddlewareService dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }

        public object CreateMicroClientProxy(Type type)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();

            object targetObject =
                proxyGenerator.CreateInterfaceProxyWithoutTarget(type, new MicroClientProxy(_dynamicMiddlewareService));
            return targetObject;
        }
    }
}