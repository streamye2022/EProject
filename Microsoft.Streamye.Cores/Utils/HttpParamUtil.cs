using System.Collections.Generic;
using System.Text;

namespace Microsoft.Streamye.Cores.Utils
{
    public class HttpParamUtil
    {
        public static string DicToHttpUrlParam(IDictionary<string, object> middleParam)
        {
            if (middleParam.Count != 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("?");

                foreach (var keyValuePair in middleParam)
                {
                    stringBuilder.Append(keyValuePair.Key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(keyValuePair.Value);
                    stringBuilder.Append("&");
                }
                
                string urlParam = stringBuilder.ToString();
                urlParam = urlParam.Substring(0, urlParam.Length - 1);
                return urlParam;
            }

            return "";
        }
    }
}