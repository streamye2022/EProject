using System.Collections.Generic;

namespace Microsoft.Streamye.Cores.DynamicMiddleware
{
    public interface IDynamicMiddlewareService
    {
        //dict参数 => 获得单个对象
        public IDictionary<string, object> Get(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public dynamic GetDynamic(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public T Get<T>(string urlShcme, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
            where T : new();

        //dict参数 => 获得多个对象
        public IList<IDictionary<string, object>> GetList(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        public IList<T> GetList<T>(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
            where T : new();

        // post
        public void Post(string urlShcme, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);
        // post 参数为dynamic的
        public dynamic PostDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam);
        // post 参数是list的
        public void Post(string urlShcme, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams);

        //删除请求
        public void Delete(string urlShcme, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);
        
        public dynamic DeleteDynamic(string urlShcme, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);

        // PUT
        public void Put(string urlShcme, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam);
        
        public dynamic PutDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam);
        
        public void Put(string urlShcme, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams);
    }
}