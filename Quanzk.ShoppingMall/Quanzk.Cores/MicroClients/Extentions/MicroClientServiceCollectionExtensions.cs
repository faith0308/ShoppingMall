using Microsoft.Extensions.DependencyInjection;
using Quanzk.Cores.DynamicMiddleware;
using Quanzk.Cores.DynamicMiddleware.Extentions;
using Quanzk.Cores.MicroClients.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients.Extentions
{
    /// <summary>
    /// 微服务客户端代理对象扩展(扩展对象注册到IOC容器)
    /// </summary>
    public static class MicroClientServiceCollectionExtensions
    {
        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="assmelyName">程序集名称</param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services, string assmelyName)
        {
            // 注册动态中台
            services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>();
            // 注册客户端代理工厂
            services.AddSingleton<MicroClientProxyFactory>();
            // 注册客户端
            services.AddSingleton<MicroClientList>();
            // 注册 MicroClient 代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClientList = serviceProvider.GetRequiredService<MicroClientList>();

            IDictionary<Type, object> dics = microClientList.GetClients(assmelyName);
            foreach (var key in dics.Keys)
            {
                services.AddSingleton(key, dics[key]);
            }
            return services;
        }

        /// <summary>
        /// 添加中台
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="options">中台服务配置对象</param>
        /// <returns></returns>
        public static IServiceCollection AddMicroClient(this IServiceCollection services, Action<MicroClientOptions> options)
        {
            MicroClientOptions microClientOptions = new MicroClientOptions();
            options(microClientOptions);

            // 注册动态中台
            services.AddDynamicMiddleware<IDynamicMiddleService, DefaultDynamicMiddleService>();
            // 注册客户端代理工厂
            services.AddSingleton<MicroClientProxyFactory>();
            // 注册客户端
            services.AddSingleton<MicroClientList>();
            // 注册 MicroClient 代理对象
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            MicroClientList microClientList = serviceProvider.GetRequiredService<MicroClientList>();

            IDictionary<Type, object> dics = microClientList.GetClients(microClientOptions.AssmelyName);
            foreach (var key in dics.Keys)
            {
                services.AddSingleton(key, dics[key]);
            }
            return services;
        }
    }
}
