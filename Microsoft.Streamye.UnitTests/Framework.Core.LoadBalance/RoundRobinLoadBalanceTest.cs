using System.Collections.Generic;
using Microsoft.Streamye.Cores.LoadBalance;
using Microsoft.Streamye.Cores.Registry;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Streamye.UnitTests.Framework.Core.LoadBalance
{
    [TestClass]
    public class RoundRobinLoadBalanceTest
    {
        [TestMethod]
        public void TestDoSelect()
        {
            RoundRobinLoadBalance robinLoadBalance = new RoundRobinLoadBalance();

            IList<ServiceNode> serviceNodes = new List<ServiceNode>()
            {
                new ServiceNode{Url = "node1"},
                new ServiceNode{Url = "node3"},
            };
        }
    }
}