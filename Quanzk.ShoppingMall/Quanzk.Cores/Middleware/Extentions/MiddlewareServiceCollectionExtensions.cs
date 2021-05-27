using Microsoft.Extensions.DependencyInjection;
using Quanzk.Cores.Middleware.Options;
using Quanzk.Cores.Pollys.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Middleware.Extentions
{
    /// <summary>
    /// 中台请求的 ServiceCollection 扩展方法
    /// </summary>
    public static class MiddlewareServiceCollectionExtensions
    {
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        ///  添加中台服务 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddMiddleware(this IServiceCollection services, Action<MiddlewareOptions> options)
        {
            MiddlewareOptions middlewareOptions = new MiddlewareOptions();
            options(middlewareOptions);

            // 注入到 IOC 容器
            services.Configure<MiddlewareOptions>(options);

            // 添加 HttpClient
            //services.AddHttpClient(middlewareOptions.HttpClientName);
            services.AddPollyHttpClient(middlewareOptions.HttpClientName, middlewareOptions.pollyHttpClientOptions);

            // 注册中台请求中间件服务 
            services.AddSingleton<IMiddleService, HttpMiddleService>();

            return services;
        }
    }
}
