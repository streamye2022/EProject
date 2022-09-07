using System;
using Consul;
using Microsoft.Extensions.Options;
using Microsoft.Streamye.Cores.Registry.Options;

namespace Microsoft.Streamye.Cores.Registry.ConsulImpl
{
    public class ConsulServiceRegistry : IServiceRegistry
    {
        private ServiceRegisterOptions _serviceRegisterOptions;
        public ConsulServiceRegistry(IOptions<ServiceRegisterOptions> options)
        {
            _serviceRegisterOptions = options.Value;
        }

        public void Register()
        {
            //get a consul client
            var consulClient = new ConsulClient(config =>
            {
                config.Address = new Uri(_serviceRegisterOptions.RegistryAddress);
            });
            
            // build uri
            var uri = new Uri(_serviceRegisterOptions.ServiceAddress);
            // build registration args
            var registration = new AgentServiceRegistration()
            {
                ID = string.IsNullOrEmpty(_serviceRegisterOptions.ServiceId) ? Guid.NewGuid().ToString() : _serviceRegisterOptions.ServiceId,
                Name = _serviceRegisterOptions.ServiceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = _serviceRegisterOptions.ServiceTags,
                Check = new AgentServiceCheck
                {
                    //consul health check timeout
                    Timeout = TimeSpan.FromSeconds(10),
                    // de register after service unavaliable
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // health check address
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{_serviceRegisterOptions.HealthCheckAddress}",
                    // consul health check interval
                    Interval = TimeSpan.FromSeconds(10),
                }
            };
            
            //register
            consulClient.Agent.ServiceRegister(registration).Wait();
            
            Console.WriteLine($"service register success: {_serviceRegisterOptions.ServiceAddress}");
            // close client
            consulClient.Dispose();
        }

        public void DeRegister()
        {
            throw new System.NotImplementedException();
        }
    }
}