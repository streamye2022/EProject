using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Streamye.Commons.Exceptions.Handlers
{
    public class BizExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BizException bizException)
            {
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = bizException.ErrorNo;
                exceptionResult.ErrorInfo = bizException.ErrorInfo;

                if (bizException.Infos != null)
                {
                    exceptionResult.infos = bizException.Infos;
                }

                context.Result = new JsonResult(exceptionResult);
            }
            else
            {
                //其他处理异常
                dynamic exceptionResult = new ExpandoObject();
                exceptionResult.ErrorNo = -1;
                exceptionResult.ErrorInfo = context.Exception.Message;

                context.Result = new JsonResult(exceptionResult);
            }
        }
    }
}