using System;
using System.Collections.Generic;
using Microsoft.Streamye.Cores.Registry;

namespace Microsoft.Streamye.Cores.LoadBalance
{
    public class RandomLoadBalance : AbstractLoadBalance
    {
        //random seed
        private readonly Random random = new Random();
        
        // 思路：前缀数组计算权重和， 然后random一个值，看看在数组中的下表
        protected override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            int length = serviceNodes.Count;
            int[] weights = new int[length];
            weights[0] = GetWeight();
            bool sameWeight = true;
            
            // 拿到前缀和数组
            for (int i = 1; i < length; i++)
            {
                weights[i] = weights[i - 1] + GetWeight();
                if (sameWeight && weights[i] != weights[0] * (i + 1))
                {
                    sameWeight = false;
                }
            }

            //走 不相等权重的 逻辑
            if (weights[length - 1] > 0 && !sameWeight)
            {
                int offset = random.Next(weights[length - 1]);
                for (int i = 0; i < weights.Length; i++)
                {
                    if (weights[i] >= offset)
                    {
                        return serviceNodes[i];
                    }
                }
            }

            return serviceNodes[random.Next(length)];
        }
    }
}