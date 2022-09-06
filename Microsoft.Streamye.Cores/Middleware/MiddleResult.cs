using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Newtonsoft.Json;

namespace Microsoft.Streamye.Cores.Middleware
{
    public class MiddleResult
    {
        public const string SUCCESS = "0";

        public string ErrorNo { get; set; }

        public string ErrorInfo { get; set; }

        public IDictionary<string, object> resultDic { set; get; }

        public IList<IDictionary<string, object>> resultList { set; get; }

        public dynamic Result { set; get; }

        public MiddleResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }
        
        public MiddleResult(string errorNo, string errorInfo)
        {
            this.ErrorNo = errorNo;
            this.ErrorInfo = errorInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }
        
        public static MiddleResult JsonToMiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
            return result;
        }
        
        public MiddleResult(string errorNo, string erroInfo, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(errorNo, erroInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
        }

    }
}