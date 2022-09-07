using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Streamye.Cores.Registry;

namespace Microsoft.Streamye.Cores.LoadBalance
{
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        private int DEFAULT_WEIGHT = 100;
        private DateTime startupTime = DateTime.Now;
        
        public ServiceNode Select(IList<ServiceNode> serviceNodes)
        {
            if (serviceNodes == null || serviceNodes.Count == 0)
            {
                return null;
            }

            if (serviceNodes.Count == 1)
            {
                return serviceNodes[0];
            }

            return DoSelect(serviceNodes);
        }

        protected abstract ServiceNode DoSelect(IList<ServiceNode> serviceNodes);


        //防止扩容时，固定权重太大的问题，看warmup的时间跟程序启动的时间相比， 按比例，如果都 warmup很久了，那么就应该是完全的权重
        private static int CalculateWarmupWeight(int warmup, int uptime, int weight)
        {
            int ww = (int) (((float)uptime / (float)warmup) * (float) weight) ;
            return ww < 1 ? 1 : (ww > weight ? weight : ww);
        }

        // 计算程序启动的时间 和 warmup所需要的时间，得出权重，默认是100，可以从配置中心拿
        protected int GetWeight()
        {
            int weight = DEFAULT_WEIGHT;
            if (weight > 0)
            {
                int uptime = DateTime.Now.Subtract(startupTime).Milliseconds;
                int warmup = 10 * 60 * 1000;

                if (uptime > 0 && uptime < warmup)
                {
                    weight = CalculateWarmupWeight(warmup,uptime,weight);
                }
            }

            return weight;
        }
    }
}