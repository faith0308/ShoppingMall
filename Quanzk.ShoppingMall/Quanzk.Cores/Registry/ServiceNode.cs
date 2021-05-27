using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry
{
    /// <summary>
    /// 服务Node,将所有服务抽象为Node
    /// </summary>
    public class ServiceNode
    {
        /// <summary>
        /// 服务url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 服务接口版本号
        /// </summary>
        public string Version { get; set; }
    }
}
