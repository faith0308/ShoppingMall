using Quanzk.Cores.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Cluster
{
    /// <summary>
    /// 服务负载均衡
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择 
        /// </summary>
        /// <param name="serviceNodes"></param>
        /// <returns></returns>
        ServiceNode Select(IList<ServiceNode> serviceNodes);
    }
}
