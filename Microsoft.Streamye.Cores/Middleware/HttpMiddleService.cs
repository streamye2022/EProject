using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Streamye.Cores.Exceptions;
using Microsoft.Streamye.Cores.Utils;
using Newtonsoft.Json;

namespace Microsoft.Streamye.Cores.Middleware
{
    public class HttpMiddleService : IMiddleService
    {

        private IHttpClientFactory _clientFactory;
        private const string httpConst = "micro";

        public HttpMiddleService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<MiddleResult> GetAsync(string middleUrl, IDictionary<string, object> middleParam)
        {
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            string urlParam = HttpParamUtil.DicToHttpUrlParam(middleParam);

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(middleUrl + urlParam);

            return await getMiddleResult(httpResponseMessage);
        }

        public async Task<MiddleResult> PostAsync(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            // 3、Post请求
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(middleUrl, httpContent);

            return await getMiddleResult(httpResponseMessage);
        }

        public async Task<MiddleResult> PostAsync(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取httpClient
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            // 3、Post请求
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(middleUrl, httpContent);

            return await getMiddleResult(httpResponseMessage);
        }

        public async Task<MiddleResult> DeleteAsync(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            // 2、Delete请求
            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(middleUrl);

            return await getMiddleResult(httpResponseMessage);
        }

        public async Task<MiddleResult> PutAsync(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");
            // 3、Put请求
            HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(middleUrl, httpContent);

            return await getMiddleResult(httpResponseMessage);
        }

        public async Task<MiddleResult> PutAsync(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取httpClient
            HttpClient httpClient = _clientFactory.CreateClient(httpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            // 3、Put请求
            HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(middleUrl, httpContent);

            return await getMiddleResult(httpResponseMessage);
        }

        private async Task<MiddleResult> getMiddleResult(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.OK) ||
                httpResponseMessage.StatusCode.Equals(HttpStatusCode.Created)||
                httpResponseMessage.StatusCode.Equals(HttpStatusCode.Accepted))
            {
                string httpJsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                return MiddleResult.JsonToMiddleResult(httpJsonString);
            }
            else
            {
                string httpErrorJsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                throw new FrameException($"{httpConst} 服务调用发生错误:{httpErrorJsonString}");
            }
        }
    }
}