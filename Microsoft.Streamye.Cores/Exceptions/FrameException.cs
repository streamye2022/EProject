using System;
using System.Collections.Generic;

namespace Microsoft.Streamye.Cores.Exceptions
{
    public class FrameException : Exception
    {
        public string ErrorNo { get; set; }

        public string ErrorInfo { get; set; }
        
        public IDictionary<string, object> Infos { set; get; }
        
        public FrameException(string errorNo, string errorInfo) : base(errorInfo)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = errorNo;
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo) : base(errorInfo)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            ErrorNo = "-1";
            ErrorInfo = errorInfo;
        }

        public FrameException(Exception e)
        {
            ErrorNo = "-1";
            ErrorInfo = e.Message;
        }
    }
}