using Microsoft.Extensions.DependencyInjection;
using Polly;
using Quanzk.Cores.Pollys.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Pollys.Extentions
{
    /// <summary>
    /// 微服务中 HttpClient 熔断，降级策略扩展
    /// </summary>
    public static class PollyHttpClientServiceCollectionExtensions
    {
        /// <summary>
        /// Httpclient扩展方法
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IServiceCollection AddPollyHttpClient(this IServiceCollection services, string name)
        {
            AddPollyHttpClient(services, name, options => { });
            return services;
        }

        /// <summary>
        /// Httpclient扩展方法
        /// </summary>
        /// <param name="services">IOC 容器</param>
        /// <param name="name">HttpClient 名称(针对不同的服务进行熔断，降级)</param>
        /// <param name="options">熔断降级配置</param>
        /// <returns>降级处理错误的结果</returns>
        public static IServiceCollection AddPollyHttpClient(this IServiceCollection services, string name, Action<PollyHttpClientOptions> options)
        {
            // 创建选项配置类
            PollyHttpClientOptions pollyHttpClientOptions = new PollyHttpClientOptions();
            options(pollyHttpClientOptions);

            // 封装降级消息
            var fallbackResponse = new HttpResponseMessage
            { // 降级消息
                Content = new StringContent(pollyHttpClientOptions.ResponseMessage),
                // 降级状态码
                StatusCode = HttpStatusCode.GatewayTimeout
            };

            // 配置HttpClient  熔断降级策略
            services.AddHttpClient(name)
                // 降级策略
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .HandleInner<Exception>()
                .FallbackAsync(fallbackResponse, async b =>
                {
                    // 降级打印异常
                    Console.WriteLine($"服务{name}开始降级,异常消息：{b.Exception.Message}");
                    // 降级后的数据
                    Console.WriteLine($"服务{name}降级内容响应：{fallbackResponse.RequestMessage}");
                    await Task.CompletedTask;
                }))
                // 断路器策略
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<Exception>()
                .CircuitBreakerAsync(pollyHttpClientOptions.CircuitBreakerOpenFallCount, TimeSpan.FromSeconds(pollyHttpClientOptions.CircuitBreakerDownTime), (ex, ts) =>
                {
                    Console.WriteLine($"服务{name}断路器开启，异常消息：{ex.Exception.Message}");
                    Console.WriteLine($"服务{name}断路器开启时间：{ts.TotalSeconds}s");

                }, () =>
                {
                    Console.WriteLine($"服务{name}断路器关闭");
                }, () =>
                {
                    Console.WriteLine($"服务{name}断路器半开启(时间控制，自动开关)");
                }))
                // 重试策略
                .AddPolicyHandler(Policy<HttpResponseMessage>
                .Handle<Exception>()
                .RetryAsync(pollyHttpClientOptions.RetryCount))
                // 超时策略
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(pollyHttpClientOptions.TimeoutTime)));

            return services;
        }
    }
}
