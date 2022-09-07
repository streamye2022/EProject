using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Streamye.Cores.Registry;
using System.Threading;

namespace Microsoft.Streamye.Cores.LoadBalance
{
    public class RoundRobinLoadBalance : AbstractLoadBalance
    {
        private static int RecycleMax = 100000;
        protected override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            string key = serviceUrls[0].Url;
            ConcurrentDictionary<string, WeightedRoundRobin> map = methodWeightMap[key];
            if (map == null)
            {
                methodWeightMap.TryAdd(key, new ConcurrentDictionary<string, WeightedRoundRobin>());
                map = methodWeightMap[key];
            }
            int totalWeight = 0;
            long maxCurrent = long.MaxValue;
            long now = DateTime.Now.ToFileTimeUtc();
            ServiceNode serviceUrl = null;
            WeightedRoundRobin selectedWRR = null;
            foreach (ServiceNode url in serviceUrls)
            {
                string identifyString = url.Url;
                WeightedRoundRobin weightedRoundRobin = map[identifyString];
                int weight = GetWeight();
                if (weight < 0)
                {
                    weight = 0;
                }
                if (weightedRoundRobin == null)
                {
                    weightedRoundRobin = new WeightedRoundRobin();
                    weightedRoundRobin.SetWeight(weight);
                    map.TryAdd(identifyString, weightedRoundRobin);
                    weightedRoundRobin = map[identifyString];
                }
                if (weight != weightedRoundRobin.GetWeight())
                {
                    //weight changed
                    weightedRoundRobin.SetWeight(weight);
                }
                long cur = weightedRoundRobin.IncreaseCurrent();
                weightedRoundRobin.SetLastUpdate(now);
                if (cur > maxCurrent)
                {
                    maxCurrent = cur;
                    serviceUrl = url;
                    selectedWRR = weightedRoundRobin;
                }
                totalWeight += weight;
            }
            if (!updateLock.get() && serviceUrls.Count != map.Count)
            {
                if (updateLock.compareAndSet(false, true))
                {
                    try
                    {
                        // copy -> modify -> update reference
                        ConcurrentDictionary<String, WeightedRoundRobin> newMap = new ConcurrentDictionary<String, WeightedRoundRobin>();
                       // newMap.TryUpdate(map);
                        IEnumerator<string> it = newMap.Keys.GetEnumerator();
                        while (it.MoveNext())
                        {
                            
                            if (now - newMap[it.Current].GetLastUpdate() > RECYCLE_PERIOD)
                            {
                                it.Dispose();
                            }
                        }
                        methodWeightMap.TryAdd(key, newMap);
                    }
                    finally
                    {
                        updateLock.set(false);
                    }
                }
            }
            if (serviceUrl != null)
            {
                selectedWRR.Sel(totalWeight);
                return serviceUrl;
            }
            // should not happen here
            return serviceUrls[0];
        }
    }
}