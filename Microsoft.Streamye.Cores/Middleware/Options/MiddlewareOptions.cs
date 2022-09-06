using System;
using Microsoft.Streamye.Cores.Pollys.Options;

namespace Microsoft.Streamye.Cores.Middleware.Options
{
    public class MiddlewareOptions
    {
        public string HttpClientName { set; get; }

        public Action<PollyHttpClientOptions> pollyHttpClientOptions { get; set; }
        
        public MiddlewareOptions()
        {
            this.HttpClientName = "Micro";
            pollyHttpClientOptions = options => { };
        }

    }
}