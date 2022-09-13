using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.DynamicMiddleware.Urls;
using Microsoft.Streamye.Cores.Exceptions;
using Microsoft.Streamye.Cores.Middleware;
using Microsoft.Streamye.Cores.Utils;

namespace Microsoft.Streamye.Cores.DynamicMiddleware
{
    public class DefaultDynamicMiddlewareService : IDynamicMiddlewareService
    {
        private IMiddleService _middleService;
        private IDynamicUrl _dynamicUrl;

        public DefaultDynamicMiddlewareService(IMiddleService middleService, IDynamicUrl dynamicUrl)
        {
            _middleService = middleService;
            _dynamicUrl = dynamicUrl;
        }


        public async Task<IDictionary<string, object>> GetAsync(string urlSchema, string serviceName,
            string serviceLink, IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.GetAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            IsSuccess(middleResult);

            return middleResult.resultDic;
        }

        public async Task<dynamic> GetDynamicAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.GetAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public async Task<T> GetAsync<T>(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam) where T : new()
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.GetAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);

            return ConvertUtil.MiddleResultDictsToObject<T>(middleResult);
        }

        public async Task<IList<IDictionary<string, object>>> GetListAsync(string urlSchema, string serviceName,
            string serviceLink, IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.GetAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);

            return middleResult.resultList;
        }

        public async Task<IList<T>> GetListAsync<T>(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam) where T : new()
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.GetAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);

            return ConvertUtil.ListToObject<T>(middleResult.resultList);
        }

        public async Task PostAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.PostAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }

        public async Task PostDynamicAsync(string urlSchema, string serviceName, string serviceLink,
            dynamic middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.PostDynamicAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }

        public async Task PostAsync(string urlSchema, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.PostAsync(targetUrl + serviceLink, middleParams);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }

        public async Task DeleteAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.DeleteAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }
        
        public async Task PutAsync(string urlSchema, string serviceName, string serviceLink,
            IDictionary<string, object> middleParam)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.PutAsync(targetUrl + serviceLink, middleParam);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }

        public async Task PutAsync(string urlSchema, string serviceName, string serviceLink,
            IList<IDictionary<string, object>> middleParams)
        {
            //获得url
            var targetUrl = await _dynamicUrl.GetMiddleUrlAsync(urlSchema, serviceName);

            //获得get结果
            MiddleResult middleResult = await _middleService.PutAsync(targetUrl + serviceLink, middleParams);

            //判断结果
            //不用try catch ,不断往上抛就可以了
            IsSuccess(middleResult);
        }

        private void IsSuccess(MiddleResult middleResult)
        {
            if (!middleResult.ErrorNo.Equals("0"))
            {
                throw new FrameException(middleResult.ErrorNo, middleResult.ErrorInfo);
            }
        }
    }
}