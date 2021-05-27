using Quanzk.Cores.DynamicMiddleware.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Options
{
    /// <summary>
    ///  客户端代理选项
    /// </summary>
    public class MicroClientOptions
    {
        public MicroClientOptions()
        {
            this.dynamicMiddlewareOptions = options => { };
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssmelyName { get; set; }

        /// <summary>
        /// 动态中台选项
        /// </summary>
        public Action<DynamicMiddlewareOptions> dynamicMiddlewareOptions { get; set; }
    }
}
