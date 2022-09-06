using System.Collections.Generic;

namespace Microsoft.Streamye.Cores.Registry
{
    public interface IServiceDiscovery
    {
        public IList<ServiceNode> Discovery(string serviceName);

        public void Refresh();
    }
}