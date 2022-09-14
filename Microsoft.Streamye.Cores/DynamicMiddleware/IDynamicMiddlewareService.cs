using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Streamye.Cores.DynamicMiddleware
{
    public interface IDynamicMiddlewareService
    {
        //dict参数 => 获得单个对象
        public Task<IDictionary<string, object>> GetAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public Task<dynamic> GetDynamicAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public Task<T> GetAsync<T>(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
            where T : new();

        //dict参数 => 获得多个对象
        public Task<IList<IDictionary<string, object>>> GetListAsync(string urlSchema, string serviceName,
            string serviceLink,
            IDictionary<string, object> middleParam);

        public Task<IList<T>> GetListAsync<T>(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
            where T : new();

        // post
        public Task PostAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        // post 参数为dynamic的
        public Task PostDynamicAsync(string urlSchema, string serviceName, string serviceLink, dynamic middleParam);

        // post 参数是list的
        public Task PostAsync(string urlSchema, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams);

        // Delete 删除请求
        public Task DeleteAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public Task<dynamic> DeleteDynamicAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        // PUT
        public Task PutAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public Task<dynamic> PutDynamicAsync(string urlSchema, string serviceName, string serviceLink,
            dynamic middleParam);

        public Task PutAsync(string urlSchema, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams);
    }
}