namespace Microsoft.Streamye.Cores.Registry.Options
{
    public class ServiceDiscoveryOptions
    {
        public string DiscoveryAddress { set; get; }

        public ServiceDiscoveryOptions()
        {
            //默认地址
            this.DiscoveryAddress = "http://localhost:8500";
        }
    }
}