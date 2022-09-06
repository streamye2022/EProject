using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Streamye.Cores.Pollys.Options;
using Polly;

namespace Microsoft.Streamye.Cores.Pollys.Extensions
{
    public static class PollyHttpClientExtensions
    {
        public static IServiceCollection AddPollyHttpClient(this IServiceCollection services, string name,
            Action<PollyHttpClientOptions> action)
        {
            //构造参数options
            PollyHttpClientOptions clientOptions = new PollyHttpClientOptions();
            action(clientOptions);
            
            //封装降级信息
            var fallbackResponse = new HttpResponseMessage
            {
                Content = new StringContent(clientOptions.DownGradeResponseMessage),
                StatusCode = HttpStatusCode.GatewayTimeout
            };

            services.AddHttpClient(name)
                //降级
                .AddPolicyHandler(Policy<HttpResponseMessage>.HandleInner<Exception>().FallbackAsync(fallbackResponse,
                    async b =>
                    {
                        Console.WriteLine($"服务{name}开始降级，异常信息:{b.Exception.Message}");
                        Console.WriteLine($"服务{name}降级响应消息:{fallbackResponse.RequestMessage}");
                        await Task.CompletedTask;
                    }))
                //熔断
                .AddPolicyHandler((Policy<HttpResponseMessage>.HandleInner<Exception>().CircuitBreakerAsync(
                    clientOptions.CircuitBreakerOpenFallCount,
                    TimeSpan.FromSeconds(clientOptions.CircuitBreakerDownTimeSeconds),
                    (ex, ts) =>
                    {
                        Console.WriteLine($"服务{name}断路器开启，异常消息：{ex.Exception.Message}");
                        Console.WriteLine($"服务{name}断路器开启时间：{ts.TotalSeconds}s");
                    }, () =>
                    {
                        Console.WriteLine($"服务{name}断路器关闭");
                    }, () =>
                    {
                        Console.WriteLine($"服务{name}断路器半开启(时间控制，自动开关)");
                    })))
                //重试
                .AddPolicyHandler(Policy<HttpResponseMessage>
                    .Handle<Exception>()
                    .RetryAsync(clientOptions.RetryCount))
                //超时
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(clientOptions.TimeoutSeconds)));
            return services;
        }
    }
}