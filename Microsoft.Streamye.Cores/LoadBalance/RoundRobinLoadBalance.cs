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

        private ConcurrentDictionary<string, ConcurrentDictionary<string, WeightedRoundRobin>> methodWeightMap =
            new ConcurrentDictionary<string, ConcurrentDictionary<string, WeightedRoundRobin>>();

        private long updateLock = 0;
        
        protected override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            string key = serviceNodes[0].Url;
            //得到 map
            ConcurrentDictionary<string, WeightedRoundRobin> map = methodWeightMap[key];
            if (map == null)
            {
                methodWeightMap.TryAdd(key, new ConcurrentDictionary<string, WeightedRoundRobin>());
                map = methodWeightMap[key];
            }
            
            //初始化变量
            int totalWeight = 0;
            long maxCurrent = long.MinValue;
            long now = DateTime.Now.ToFileTimeUtc();
            ServiceNode serviceUrl = null;
            WeightedRoundRobin selectedWRR = null;
            
            //循环处理所有的nodes
            foreach (ServiceNode node in serviceNodes)
            {
                //初始化map中的 WeightedRoundRobin 对象
                string identifyString = node.Url;
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
                
                //weight changed
                if (weight != weightedRoundRobin.GetWeight())
                {
                    weightedRoundRobin.SetWeight(weight);
                }
                long cur = weightedRoundRobin.IncreaseCurrent();
                weightedRoundRobin.SetLastUpdate(now);
                if (cur > maxCurrent)
                {
                    maxCurrent = cur;
                    serviceUrl = node;
                    selectedWRR = weightedRoundRobin;
                }
                totalWeight += weight;
            }
            
            
            if (Interlocked.Read(ref updateLock)!=0 && serviceNodes.Count != map.Count)
            {
                if (Interlocked.CompareExchange(ref updateLock,1,0) == 1)
                {
                    try
                    {
                        // copy -> modify -> update reference
                        ConcurrentDictionary<String, WeightedRoundRobin> newMap = new ConcurrentDictionary<String, WeightedRoundRobin>();
                       // newMap.TryUpdate(map);
                        IEnumerator<string> it = newMap.Keys.GetEnumerator();
                        while (it.MoveNext())
                        {
                            
                            if (now - newMap[it.Current].GetLastUpdate() > RecycleMax)
                            {
                                it.Dispose();
                            }
                        }
                        methodWeightMap.TryAdd(key, newMap);
                    }
                    finally
                    {
                        Interlocked.Add(ref updateLock, -1);
                    }
                }
            }
            if (serviceUrl != null)
            {
                selectedWRR.DecreaseCurrent(totalWeight);
                return serviceUrl;
            }
            // should not happen here
            return serviceNodes[0];
        }
    }
}