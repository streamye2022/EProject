using System;
using Microsoft.Streamye.Cores.DynamicMiddleware.options;

namespace Microsoft.Streamye.Cores.MicroClients.Options
{
    public class MicroClientOptions
    {
        public MicroClientOptions()
        {
            //如果默认，那就啥都不做，这个option肯定不能为空，否则就出问题了
            dynamicMiddlewareOption = option => { };
        }

        public string AssemblyName { set; get; }
        public Action<DynamicMiddlewareOption> dynamicMiddlewareOption { set; get; }
    }
}