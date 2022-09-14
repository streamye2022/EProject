using System;

namespace Microsoft.Streamye.Cores.MicroClients.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class MicroClient : Attribute
    {
        public string UrlSchema { get; }
        public string ServiceName { get; }

        public MicroClient(string urlSchema, string serviceName)
        {
            this.UrlSchema = urlSchema;
            ServiceName = serviceName;
        }
    }
}