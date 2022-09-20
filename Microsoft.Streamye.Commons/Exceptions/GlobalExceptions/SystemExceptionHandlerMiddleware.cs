using System.Dynamic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Streamye.Commons.Exceptions.GlobalExceptions
{
    public class SystemExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public SystemExceptionHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        //拦截异常
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, httpContext.Response.StatusCode, ex.Message);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            context.Response.ContentType = "application/json;charset=utf-8";

            // 1、异常结果转换成json格式输出
            dynamic warpResult = new ExpandoObject();
            warpResult.ErrorNo = "-1";
            warpResult.ErrorInfo = msg;

            // 2、异常json格式输出
            var stream = context.Response.Body;
            await JsonSerializer.SerializeAsync(stream, warpResult);
        }
    }
}