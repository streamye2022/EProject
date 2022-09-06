namespace Microsoft.Streamye.Cores.Pollys.Options
{
    public class PollyHttpClientOptions
    {
        public int TimeoutSeconds { set; get; }

        public int RetryCount { set; get; }

        public int CircuitBreakerOpenFallCount { set; get; }

        public int CircuitBreakerDownTimeSeconds { set; get; }

        public string DownGradeResponseMessage { set; get; }

        public PollyHttpClientOptions()
        {
            this.TimeoutSeconds = 60;
            this.RetryCount = 3;
            this.CircuitBreakerDownTimeSeconds = 2;
            this.CircuitBreakerOpenFallCount = 5;
            this.DownGradeResponseMessage = "服务熔断了";
        }
    }
}