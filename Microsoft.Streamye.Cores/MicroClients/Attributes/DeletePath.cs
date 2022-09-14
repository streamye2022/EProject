using System;

namespace Microsoft.Streamye.Cores.MicroClients.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePath : Attribute
    {
        public string Path { get; }

        public DeletePath(string Path)
        {
            this.Path = Path;
        }
    }
}