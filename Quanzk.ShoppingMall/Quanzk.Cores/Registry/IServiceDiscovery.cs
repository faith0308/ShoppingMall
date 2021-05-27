using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry
{
    /// <summary>
    /// 服务发现 接口
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        List<ServiceNode> Discovery(string serviceName);
    }
}
