using System.Collections.Generic;
using Microsoft.Streamye.Cores.Registry;

namespace Microsoft.Streamye.Cores.LoadBalance
{
    public interface ILoadBalance
    {
        ServiceNode Select(IList<ServiceNode> serviceNodes);
    }
}