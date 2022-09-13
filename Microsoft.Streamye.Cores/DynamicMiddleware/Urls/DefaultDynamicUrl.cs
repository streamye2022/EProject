using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.LoadBalance;
using Microsoft.Streamye.Cores.Registry;

namespace Microsoft.Streamye.Cores.DynamicMiddleware.Urls
{
    public class DefaultDynamicUrl : IDynamicUrl
    {
        private readonly IServiceDiscovery serviceDiscovery;
        private readonly ILoadBalance loadBalance;

        public DefaultDynamicUrl(IServiceDiscovery serviceDiscovery, ILoadBalance loadBalance)
        {
            this.serviceDiscovery = serviceDiscovery;
            this.loadBalance = loadBalance;
        }

        public async Task<string> GetMiddleUrlAsync(string urlScheme, string serviceName)
        {
            //服务发现
            var urls = await serviceDiscovery.DiscoveryAsync(serviceName);

            //拿到target service node
            var targetUrl = loadBalance.Select(urls);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(urlScheme);
            stringBuilder.Append("://");
            stringBuilder.Append(targetUrl.Url);
            return stringBuilder.ToString();
        }
    }
}