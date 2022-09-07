using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Streamye.Cores.Registry
{
    public interface IServiceDiscovery
    {
        public Task<IList<ServiceNode>> DiscoveryAsync(string serviceName);

        public void Refresh();
    }
}