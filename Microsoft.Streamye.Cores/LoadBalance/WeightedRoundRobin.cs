using System.Threading;

namespace Microsoft.Streamye.Cores.LoadBalance
{
    public class WeightedRoundRobin
    {
        private  int weight;
        private  long current;
        private  long lastUpdate;
        
        public  int GetWeight()
        {
            return weight;
        }
        public void SetWeight(int weight)
        {
            this.weight = weight;
            current = Interlocked.Add(ref current, 0); //
        }
        public long IncreaseCurrent()
        {
            return Interlocked.Add(ref current, weight);
        }
        public long DecreaseCurrent(int total)
        {
            return Interlocked.Add(ref current, -1 * total);
        }
        public long GetLastUpdate()
        {
            return lastUpdate;
        }
        public  void SetLastUpdate(long lastUpdate)
        {
            this.lastUpdate = lastUpdate;
        }
        
    }
}