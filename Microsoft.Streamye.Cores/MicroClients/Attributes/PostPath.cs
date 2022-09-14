using System;

namespace Microsoft.Streamye.Cores.MicroClients.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PostPath : Attribute
    {
        public string Path { get; }

        public PostPath(string Path)
        {
            this.Path = Path;
        }
    }
}