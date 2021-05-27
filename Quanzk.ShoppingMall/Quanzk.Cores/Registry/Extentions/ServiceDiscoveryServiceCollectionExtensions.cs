using Microsoft.Extensions.DependencyInjection;
using Quanzk.Cores.Registry.Consul;
using Quanzk.Cores.Registry.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry.Extentions
{
    /// <summary>
    /// 服务发现 IOC容器 扩展
    /// </summary>
    public static class ServiceDiscoveryServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, Action<ServiceDiscoveryOptions> options)
        {
            ServiceDiscoveryOptions serviceDiscoveryOptions = new ServiceDiscoveryOptions();
            options(serviceDiscoveryOptions);

            // 注册到 IOC容器
            services.Configure<ServiceDiscoveryOptions>(options);

            // 注册 Consul 服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();

            return services;
        }
    }
}
