using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Options;
using Microsoft.Streamye.Cores.Exceptions;
using Microsoft.Streamye.Cores.Registry.Options;

namespace Microsoft.Streamye.Cores.Registry.ConsulImpl
{
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        //cache
        private IDictionary<string, IList<ServiceNode>> localCache = new Dictionary<string, IList<ServiceNode>>();

        private readonly ServiceDiscoveryOptions _serviceDiscoveryOptions;

        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryOptions> options)
        {
            _serviceDiscoveryOptions = options.Value;
            
            //client
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_serviceDiscoveryOptions.DiscoveryAddress);
            });

            //get all services
            var queryResults = consulClient.Catalog.Services().Result;
            if (queryResults.StatusCode != HttpStatusCode.OK)
            {
                throw new FrameException($"consul connect get all services failed: {queryResults.StatusCode}");
            }
            
            //cache
            foreach (var keyValuePair in queryResults.Response)
            {
                var queryResult = consulClient.Catalog.Service(keyValuePair.Key).Result;
                if (queryResults.StatusCode != HttpStatusCode.OK)
                {
                    throw new FrameException($"consul connect failed: {queryResults.StatusCode}");
                }
                var list = new List<ServiceNode>();
                foreach (var catalogService in queryResult.Response)
                {
                    list.Add(new ServiceNode{Url = catalogService.Address+":"+catalogService.ServicePort});
                }

                localCache[keyValuePair.Key] = list;
            }
            
        }
        

        public async Task<IList<ServiceNode>> DiscoveryAsync(string serviceName)
        {
            //cache return
            if (localCache.ContainsKey(serviceName))
            {
                return localCache[serviceName];
            }
            //remote get
            CatalogService[] remoteResult = await RemoteDiscovery(serviceName);

            var list = new List<ServiceNode>();
            foreach (var catalogService in remoteResult)
            {
                list.Add(new ServiceNode{Url = catalogService.Address+":"+catalogService.ServicePort});
            }

            return list;
        }

        private async Task<CatalogService[]> RemoteDiscovery(string serviceName)
        {
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_serviceDiscoveryOptions.DiscoveryAddress);
            });

            var queryResult = await consulClient.Catalog.Service(serviceName);
            if (queryResult.StatusCode != HttpStatusCode.OK)
            {
                throw new FrameException($"consul connect failed: {queryResult.StatusCode}");
            }

            return queryResult.Response;
        }

        public void Refresh()
        {
            //client
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_serviceDiscoveryOptions.DiscoveryAddress);
            });

            //get all services
            var queryResults = consulClient.Catalog.Services().Result;
            if (queryResults.StatusCode != HttpStatusCode.OK)
            {
                throw new FrameException($"consul connect get all services failed: {queryResults.StatusCode}");
            }
            
            //clear cache
            localCache.Clear();
            
            //cache
            foreach (var keyValuePair in queryResults.Response)
            {
                var queryResult = consulClient.Catalog.Service(keyValuePair.Key).Result;
                if (queryResults.StatusCode != HttpStatusCode.OK)
                {
                    throw new FrameException($"consul connect failed: {queryResults.StatusCode}");
                }
                var list = new List<ServiceNode>();
                foreach (var catalogService in queryResult.Response)
                {
                    list.Add(new ServiceNode{Url = catalogService.Address+":"+catalogService.ServicePort});
                }

                localCache[keyValuePair.Key] = list;
            }
        }
    }
}