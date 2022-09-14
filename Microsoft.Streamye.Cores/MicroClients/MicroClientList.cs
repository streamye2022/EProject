using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Streamye.Cores.MicroClients
{
    public class MicroClientList
    {
        private MicroClientProxyFactory _microClientProxyFactory;
        public MicroClientList(MicroClientProxyFactory microClientProxyFactory)
        {
            _microClientProxyFactory = microClientProxyFactory;
        }

        public IDictionary<Type, object> GetClients(string assemblyName)
        {
            //加载所有 microclient特性的type
            IList<Type> types = AssemblyUtil.GetMicroClientTypesByAssemblyName(assemblyName);
            
            
            //创建所有的type
            IDictionary<Type, object> typesObjects = new Dictionary<Type, object>();

            foreach (var type in types)
            {
                object o= _microClientProxyFactory.CreateMicroClientProxy(type);
                typesObjects.Add(type,o);
            }

            return typesObjects;
        }
    }
}