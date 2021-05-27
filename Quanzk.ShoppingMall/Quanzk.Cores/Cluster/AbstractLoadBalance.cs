using Quanzk.Cores.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Cluster
{
    /// <summary>
    /// 负载均衡抽象实现
    /// </summary>
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        /// <summary>
        /// 节点选择
        /// </summary>
        /// <param name="serviceNodes"></param>
        /// <returns></returns>
        public ServiceNode Select(IList<ServiceNode> serviceNodes)
        {
            if (serviceNodes == null || serviceNodes.Count == 0)
                return null;
            if (serviceNodes.Count == 1)
                return serviceNodes[0];
            return DoSelect(serviceNodes);
        }

        /// <summary>
        /// 交由子类去实现
        /// </summary>
        /// <param name="serviceNodes"></param>
        /// <returns></returns>
        public abstract ServiceNode DoSelect(IList<ServiceNode> serviceNodes);

        /// <summary>
        /// 权重计算
        /// </summary>
        /// <param name="uptime"></param>
        /// <param name="warmup"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public static int CalculateWarmupWeight(int uptime, int warmup, int weight)
        {
            int ww = (int)((float)uptime / (float)warmup / (float)weight);
            return ww < 1 ? 1 : (ww > weight ? weight : ww);
        }

        /// <summary>
        /// 获取权重
        /// </summary>
        /// <returns></returns>
        protected int GetWeight()
        {
            int weight = 100;
            if (weight > 0)
            {
                long timestamp = 0L;
                if (timestamp > 0L)
                {
                    int uptime = (int)(DateTime.Now.ToFileTimeUtc() - timestamp);
                    int warmup = 10 * 60 * 1000;
                    if (uptime > 0 && uptime < warmup)
                    {
                        weight = CalculateWarmupWeight(uptime, warmup, weight);
                    }
                }
            }
            return weight;
        }
    }
}
