using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.Streamye.Commons.CommonResults
{
    public class CommonResult
    {
        public const string SUCCESS = "0";

        public string ErrorNo { get; set; }

        public string ErrorInfo { get; set; }

        public IDictionary<string, object> resultDic { set; get; }

        public IList<IDictionary<string, object>> resultList { set; get; }

        public dynamic Result { set; get; }

        public CommonResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public CommonResult(string errorNo, string errorInfo)
        {
            this.ErrorNo = errorNo;
            this.ErrorInfo = errorInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public static CommonResult JsonToMiddleResult(string jsonStr)
        {
            CommonResult result = JsonConvert.DeserializeObject<CommonResult>(jsonStr);
            return result;
        }

        public CommonResult(string errorNo, string erroInfo, IDictionary<string, object> resultDic,
            IList<IDictionary<string, object>> resultList) : this(errorNo, erroInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}