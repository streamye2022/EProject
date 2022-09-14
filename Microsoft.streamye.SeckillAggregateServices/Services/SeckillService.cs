using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.DynamicMiddleware;

namespace Microsoft.Streamye.SeckillAggregateServices.Services
{
    public class SeckillService
    {
        private string scheme = "http";
        private string serviceName = "SeckillService";
        private IDynamicMiddlewareService _dynamicMiddlewareService;

        public SeckillService(IDynamicMiddlewareService dynamicMiddlewareService)
        {
            _dynamicMiddlewareService = dynamicMiddlewareService;
        }
        
        // 1.一个 service的 client 2.一个const保存serviceLink 常量， 3.一个Convent类将函数的参数进行转换成dicts 
        // 问题： 太多实现类了，其实他们的功能是一样的
        public async Task<IList<object>> PostSeckills(string serviceLink, IDictionary<string, object> @params)
        {
            var listAsync =
                await _dynamicMiddlewareService.GetListAsync<object>(scheme, serviceName, serviceLink, @params);
            return listAsync;
        }
    }
}