using System;
using System.Collections.Generic;

namespace Microsoft.Streamye.Commons.Exceptions
{
    public class BizException : Exception
    {
        public string ErrorNo { get; set; }

        public string ErrorInfo { get; set; }

        public IDictionary<string, object> Infos { set; get; }

        public BizException(string errorNo, string errorInfo) : base(errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public BizException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public BizException(string errorInfo) : base(errorInfo)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public BizException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public BizException(Exception e)
        {
            ErrorNo = "-1";
            ErrorInfo = e.Message;
        }
    }
}