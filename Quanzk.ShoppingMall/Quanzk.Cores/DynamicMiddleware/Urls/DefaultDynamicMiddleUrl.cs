using Quanzk.Cores.Cluster;
using Quanzk.Cores.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware.Urls
{
    /// <summary>
    /// 默认获取Url
    /// </summary>
    public class DefaultDynamicMiddleUrl : IDynamicMiddleUrl
    {
        private readonly IServiceDiscovery serviceDiscovery;
        private readonly ILoadBalance loadBalance;

        public DefaultDynamicMiddleUrl(IServiceDiscovery serviceDiscovery, ILoadBalance loadBalance)
        {
            this.serviceDiscovery = serviceDiscovery;
            this.loadBalance = loadBalance;
        }

        /// <summary>
        /// 获取中台服务请求Url地址
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public string GetMiddleUrl(string urlShcme, string serviceName)
        {
            // 获取服务节点
            IList<ServiceNode> serviceNodes = serviceDiscovery.Discovery(serviceName);
            // 选择负载均衡算法
            ServiceNode serviceNode = loadBalance.Select(serviceNodes);
            // 构建Url
            StringBuilder sbStr = new StringBuilder();
            sbStr.Append(urlShcme);
            sbStr.Append("://");
            sbStr.Append(serviceNode.Url);
            return sbStr.ToString();
        }
    }
}
