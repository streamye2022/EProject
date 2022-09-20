using System.Collections;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Streamye.Commons.CommonResults
{
    public class AggregateCommonResultHandler : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult)
            {
                int? statusCode = objectResult.StatusCode;
                if (statusCode == 200
                    || statusCode == 201
                    || statusCode == 202
                    || !statusCode.HasValue)
                {
                    objectResult.Value = wrapSuccessResult(objectResult.Value);
                }
                else
                {
                    objectResult.Value = wrapFailedResult(objectResult);
                }
            }

            await next();
        }

        private object wrapFailedResult(ObjectResult objectResult)
        {
            dynamic wrapResult = new ExpandoObject();
            wrapResult.ErrorNo = objectResult.StatusCode;
            if (objectResult.Value is string info)
            {
                wrapResult.ErrorInfo = info;
            }
            else
            {
                wrapResult.ErrorInfo = new JsonResult(objectResult.Value).Value;
            }

            return wrapResult;
        }

        private object wrapSuccessResult(object value)
        {
            dynamic wrapResult = new ExpandoObject();
            wrapResult.ErrorNo = "0";
            wrapResult.ErrorInfo = "";
            //是否是list
            if (value.GetType().Name.Contains("List"))
            {
                // 转换成json
                wrapResult.ResultList = new JsonResult(value).Value;
            }
            //是否是字典
            else if (value.GetType().Name.Contains("Dictionary"))
            {
                IDictionary dictionary = (IDictionary)value;
                if (dictionary.Contains("ErrorInfo"))
                {
                    wrapResult.ErrorNo = dictionary["ErrorNo"];
                    wrapResult.ErrorInfo = dictionary["ErrorInfo"];
                    dictionary.Remove("ErrorNo");
                    dictionary.Remove("ErrorInfo");
                }

                wrapResult.ResultDic = new JsonResult(value).Value;
            }
            else
            {
                wrapResult.Result = new JsonResult(value).Value;
            }

            return wrapResult;
        }
    }
}