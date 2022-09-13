using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Microsoft.Streamye.Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.Streamye.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void TestApiVersionConfigHelpers()
        {
            Console.WriteLine(ApiVersionConfigHelpers.ArmAPIVersion);
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            MyClass myClass = new MyClass();
            myClass.SetMyEnum(CheckErrorType.DnsCheckFqdnNotFound);

            Console.WriteLine(JsonConvert.SerializeObject(myClass));
        }
        
        [JsonConverter(typeof(StringEnumConverter))]
        // [DataContract(Name = "CheckErrorType")]
        public enum CheckErrorType
        {
            [EnumMember(Value = "dnsCheckFqdnNotFound")]
            DnsCheckFqdnNotFound = 100,
            
            [EnumMember(Value = "dnsCheckNameWithInvalidCharacter")]
            DnsCheckNameWithInvalidCharacter = 101,

        }

        public class MyClass
        {
            [JsonProperty("errorType")]
            private CheckErrorType _myEnum;

            public void SetMyEnum(CheckErrorType myEnum)
            {
                _myEnum = myEnum;
            }
        }

    }
}