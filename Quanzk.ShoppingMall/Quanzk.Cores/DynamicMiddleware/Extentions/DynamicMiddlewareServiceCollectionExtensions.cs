using Microsoft.Extensions.DependencyInjection;
using Quanzk.Cores.Cluster.Extentions;
using Quanzk.Cores.DynamicMiddleware.Options;
using Quanzk.Cores.DynamicMiddleware.Urls;
using Quanzk.Cores.Middleware.Extentions;
using Quanzk.Cores.Registry.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware.Extentions
{
    /// <summary>
    /// 中台ServiceCollection扩展方法
    /// </summary>
    public static class DynamicMiddlewareServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="TMiddleService"></typeparam>
        /// <typeparam name="TMiddleImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services) where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(services, options => { });
            return services;
        }

        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="TMiddleService"></typeparam>
        /// <typeparam name="TMiddleImplementation"></typeparam>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services, Action<DynamicMiddlewareOptions> options) where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            DynamicMiddlewareOptions dynamicMiddlewareOptions = new DynamicMiddlewareOptions();
            options(dynamicMiddlewareOptions);

            // 注册服务发现
            services.AddServiceDiscovery(dynamicMiddlewareOptions.serviceDiscoveryOptions);

            // 注册负载均衡
            services.AddLoadBalance(dynamicMiddlewareOptions.loadBalanceOptions);

            // 添加中台服务
            services.AddMiddleware(dynamicMiddlewareOptions.middlewareOptions);

            // 注册动态中台Url服务
            services.AddSingleton<IDynamicMiddleUrl, DefaultDynamicMiddleUrl>();

            // 注册动态服务
            services.AddSingleton<TMiddleService, TMiddleImplementation>();
            return services;
        }
    }
}
