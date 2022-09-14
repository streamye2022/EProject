using System;

namespace Microsoft.Streamye.Cores.MicroClients.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GetPath : Attribute
    {
        public string Path { get; }

        public GetPath(string Path)
        {
            this.Path = Path;
        }
    }
}