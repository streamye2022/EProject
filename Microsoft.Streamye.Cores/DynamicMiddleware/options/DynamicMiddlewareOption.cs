using System;
using Microsoft.Streamye.Cores.LoadBalance.Options;
using Microsoft.Streamye.Cores.Middleware.Options;
using Microsoft.Streamye.Cores.Registry.Options;

namespace Microsoft.Streamye.Cores.DynamicMiddleware.options
{
    public class DynamicMiddlewareOption
    {
        public DynamicMiddlewareOption()
        {
            serviceDiscoveryOptions = options => { };
            loadBalanceOptions = options => { };
            middlewareOptions = options => { };
        }

        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { set; get; }

        public Action<LoadBalanceOptions> loadBalanceOptions { set; get; }

        public Action<MiddlewareOptions> middlewareOptions { set; get; }
    }
}