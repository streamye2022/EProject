namespace Microsoft.Streamye.Cores.LoadBalance.Options
{
    public class LoadBalanceOptions
    {
        public LoadBalanceOptions()
        {
            this.Type = "Random";
        }

        public string Type { set; get; }
    }
}