using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace Microsoft.Streamye.Commons
{
    public class ApiVersionConfigHelpers
    {
        public static readonly string ArmAPIVersion = "2020-06-01";

        public static readonly string ResourceGroupApiVersion = "2021-04-01";

        // private static IDictionary<string, string> versionDicts = new Dictionary<string, string>();

        static ApiVersionConfigHelpers() {
            Type type = typeof(ApiVersionConfigHelpers);
            IEnumerable<FieldInfo> fields = type.GetFields().Where(info => info.IsInitOnly);
            foreach (var field in fields)
            {
                Console.WriteLine(field.Name + "=" +field.GetValue(null));
                // // fieldInfo.SetValue(this, EnvironmentVariableUtility.GetWithDefaultValue(fieldInfo.Name, fieldInfo.GetValue(this).ToString()));
                field.SetValue(null,"aaaa");
                Console.WriteLine(field.Name + "=" + field.GetValue(null));
            }
        }
        


        // public ApiVersionConfigHelpers()
        // {
        //     Type type = typeof(ApiVersionConfigHelpers);
        //     FieldInfo[] fields = type.GetFields();
        //     foreach (var fieldInfo in fields)
        //     {
        //         Console.WriteLine(fieldInfo.Name + "=" + fieldInfo.GetValue(this).ToString());
        //         // fieldInfo.SetValue(this, EnvironmentVariableUtility.GetWithDefaultValue(fieldInfo.Name, fieldInfo.GetValue(this).ToString()));
        //         fieldInfo.SetValue(this,"dadasdas");
        //         Console.WriteLine(fieldInfo.Name + "=" + fieldInfo.GetValue(this).ToString());
        //     }
        // }



        //
        // public string this[string key]
        // {
        //     get
        //     {
        //         return versionDicts[key];
        //     }
        // }


        // private static class CanaryDefaultApiVersionConstants
        // {
        //     /// <summary>
        //     /// Version of the Azure REST API.
        //     /// </summary>
        //     public const string ArmAPIVersion = "2020-06-01";
        //     
        //     /// <summary>
        //     /// Get ResourceGroup default API version
        //     /// </summary>
        //     public const string ResourceGroupApiVersion = "2021-04-01";
        // }
    }
}