using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry.Options
{
    /// <summary>
    /// 服务发现选项
    /// </summary>
    public class ServiceDiscoveryOptions
    {
        public ServiceDiscoveryOptions()
        {
            this.DiscoveryAddress = "http://localhost:8500";
        }

        /// <summary>
        /// 服务发现地址
        /// </summary>
        public string DiscoveryAddress { get; set; }
    }
}
