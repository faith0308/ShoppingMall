using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry.Options
{
    /// <summary>
    /// 节点注册选项
    /// </summary>
    public class ServiceRegistryOptions
    {
        public ServiceRegistryOptions()
        {
            this.ServiceId = Guid.NewGuid().ToString();
            this.RegistryAddress = "http://localhost:8500";
            this.HealthCheckAddress = "/HealthCheck";
        }

        /// <summary>
        /// 服务Id
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务地址 https://localhost:port
        /// </summary>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// 服务标签 （版本）
        /// </summary>
        public string[] ServiceTags { get; set; }

        /// <summary>
        /// 服务端口号（可选项 ---> 默认加载启动路径端口）
        /// </summary>
        public int ServicePort { get; set; }

        /// <summary>
        /// Https 或者 http
        /// </summary>
        public string ServiceScheme { get; set; }

        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string RegistryAddress { get; set; }

        /// <summary>
        /// 服务健康检查地址
        /// </summary>
        public string HealthCheckAddress { get; set; }
    }
}
