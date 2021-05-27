using Quanzk.Cores.Cluster.Options;
using Quanzk.Cores.Middleware.Options;
using Quanzk.Cores.Registry.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware.Options
{
    /// <summary>
    /// 中台配置选项
    /// </summary>
    public class DynamicMiddlewareOptions
    {
        public DynamicMiddlewareOptions()
        {
            serviceDiscoveryOptions = options => { };
            loadBalanceOptions = options => { };
            middlewareOptions = Options => { };
        }

        /// <summary>
        /// 服务发现选项
        /// </summary>
        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { get; set; }

        /// <summary>
        /// 负载均衡选项
        /// </summary>
        public Action<LoadBalanceOptions> loadBalanceOptions { get; set; }

        /// <summary>
        /// 中台选项
        /// </summary>
        public Action<MiddlewareOptions> middlewareOptions { get; set; }
    }
}
