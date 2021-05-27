using Quanzk.Cores.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Cluster
{
    /// <summary>
    /// 加权随机算法
    /// </summary>
    public class RandomLoadBalance : AbstractLoadBalance
    {
        private readonly Random random = new Random();
        public override ServiceNode DoSelect(IList<ServiceNode> serviceNodes)
        {
            // 服务节点数量
            int length = serviceNodes.Count;
            // 权重总和
            int totalWeight = 0;
            // 是否设置每个服务节点具有相同的权重
            bool sameWeight = true;

            for (int i = 0; i < length; i++)
            {
                int weight = GetWeight();
                totalWeight += weight;
                if (sameWeight && i > 0 && weight != GetWeight())
                {
                    sameWeight = false;
                }
            }
            if (totalWeight > 0 && !sameWeight)
            {
                // 如果(不是每个调用程序都有相同的权重&至少有一个调用程序的权重大于0)，则根据totalWeight随机选择。
                int offset = random.Next(totalWeight);
                // 根据随机值返回一个调用程序
                for (int i = 0; i < length; i++)
                {
                    offset -= GetWeight();
                    if (offset < 0)
                    {
                        return serviceNodes[i];
                    }
                }
            }
            // 如果所有调用方都有相同的weight值或totalWeight=0
            return serviceNodes[random.Next(length)];
        }
    }
}
