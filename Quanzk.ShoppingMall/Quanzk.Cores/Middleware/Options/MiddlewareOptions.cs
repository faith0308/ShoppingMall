using Quanzk.Cores.Pollys.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Middleware.Options
{
    /// <summary>
    /// 中台请求配置项 
    /// </summary>
    public class MiddlewareOptions
    {
        public MiddlewareOptions()
        {
            this.HttpClientName = "Micro";
            this.pollyHttpClientOptions = options => { };
        }

        /// <summary>
        /// 客户端名称
        /// </summary>
        public string HttpClientName { get; set; }

        /// <summary>
        /// polly熔断降级选项
        /// </summary>
        public Action<PollyHttpClientOptions> pollyHttpClientOptions { get; set; }
    }
}
