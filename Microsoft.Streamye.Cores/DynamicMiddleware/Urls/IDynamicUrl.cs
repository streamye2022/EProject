using System.Threading.Tasks;

namespace Microsoft.Streamye.Cores.DynamicMiddleware.Urls
{
    public interface IDynamicUrl
    {
        public Task<string> GetMiddleUrlAsync(string urlScheme, string serviceName);
    }
}