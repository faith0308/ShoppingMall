using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Attributes
{
    /// <summary>
    /// 微服务客户端特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class MicroClient : Attribute
    {
        /// <summary>
        /// Url地址
        /// </summary>
        public string UrlShcme { get; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// 构造函数  注入
        /// </summary>
        /// <param name="urlShcme"></param>
        /// <param name="serviceName"></param>
        public MicroClient(string urlShcme, string serviceName)
        {
            this.UrlShcme = urlShcme;
            this.ServiceName = serviceName;
        }
    }
}
