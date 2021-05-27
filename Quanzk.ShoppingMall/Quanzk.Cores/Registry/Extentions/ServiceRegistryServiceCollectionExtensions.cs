using Microsoft.Extensions.DependencyInjection;
using Quanzk.Cores.Registry.Consul;
using Quanzk.Cores.Registry.Options;
using System;

namespace Quanzk.Cores.Registry.Extentions
{
    /// <summary>
    /// 服务注册 IOC 容器扩展
    /// </summary>
    public static class ServiceRegistryServiceCollectionExtensions
    {
        /// <summary>
        /// Consul服务注册
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services)
        {
            AddServiceRegistry(services, options => { });
            return services;
        }

        /// <summary>
        /// Consul服务注册
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="options">选项</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceRegistry(this IServiceCollection services, Action<ServiceRegistryOptions> options)
        {
            ServiceRegistryOptions serviceRegistryOptions = new ServiceRegistryOptions();
            options(serviceRegistryOptions);

            // 配置 选项到 IOC
            services.Configure<ServiceRegistryOptions>(options);

            // 注册 Consul
            //services.AddSingleton<IServiceRegistry, ConsulServiceRegistry>();
            services.AddSingleton(typeof(IServiceRegistry), typeof(ConsulServiceRegistry));

            // 注册开机自动注册服务
            services.AddHostedService<ServiceRegistryIHostedService>();

            return services;
        }
    }
}
