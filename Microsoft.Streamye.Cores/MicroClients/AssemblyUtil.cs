using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Streamye.Cores.MicroClients.Attributes;

namespace Microsoft.Streamye.Cores.MicroClients
{
    public class AssemblyUtil
    {
        public static IList<Type> GetMicroClientTypesByAssemblyName(string assemblyName)
        {
            IList<Type> typesResult = new List<Type>();
            
            // 两种加载 assembly 的方法
            // Assembly assembly = Assembly.Load("Microsoft.Streamye.DesignPattern");
            // Type[] types = assembly.GetTypes();
            
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeinfos = assembly.GetTypes();
            foreach (var typeinfo in typeinfos)
            {
                MicroClient microClient = typeinfo.GetCustomAttribute<MicroClient>();
                if (microClient != null)
                {
                    typesResult.Add(typeinfo);
                }
            }

            return typesResult;
        }
    }
}