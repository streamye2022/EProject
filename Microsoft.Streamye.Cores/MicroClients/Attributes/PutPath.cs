using System;

namespace Microsoft.Streamye.Cores.MicroClients.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PutPath : Attribute
    {
        public string Path { get; }

        public PutPath(string Path)
        {
            this.Path = Path;
        }
    }
}