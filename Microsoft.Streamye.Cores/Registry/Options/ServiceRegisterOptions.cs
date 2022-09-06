namespace Microsoft.Streamye.Cores.Registry.Options
{
    public class ServiceRegisterOptions
    {
        public string ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string ServiceAddress { get; set; }

        public string[] ServiceTags { get; set; }

        public string RegistryAddress { get; set; }

        public string HealthCheckAddress { get; set; }
    }
}