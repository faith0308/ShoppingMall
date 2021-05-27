using Consul;
using Microsoft.Extensions.Options;
using Quanzk.Cores.Exceptions;
using Quanzk.Cores.Registry.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry.Consul
{
    /// <summary>
    /// consul 服务注册实现
    /// </summary>
    public class ConsulServiceRegistry : IServiceRegistry
    {
        /// <summary>
        /// 服务注册选项
        /// </summary>
        public readonly ServiceRegistryOptions serviceRegistryOptions;

        public ConsulServiceRegistry(IOptions<ServiceRegistryOptions> serviceRegistryOptions)
        {
            this.serviceRegistryOptions = serviceRegistryOptions.Value;
        }

        /// <summary>
        /// 注销服务
        /// </summary>
        public void Deregister()
        {
            // 创建Consul 客户端链接
            var consulClient = new ConsulClient(configuration =>
            {
                // 建立客户端和服务端连接
                configuration.Address = new Uri(serviceRegistryOptions.RegistryAddress);
            });

            // 注销服务
            consulClient.Agent.ServiceDeregister(serviceRegistryOptions.ServiceId).Wait();

            Console.WriteLine($"服务注销成功：{serviceRegistryOptions.ServiceAddress}");

            // 关闭连接
            consulClient.Dispose();
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        public void Register()
        {
            // 创建Consul 客户端链接
            var consulClient = new ConsulClient(configuration =>
            {
                // 建立客户端和服务端链接
                configuration.Address = new Uri(serviceRegistryOptions.RegistryAddress);
            });
            // 获取服务地址
            var uri = new Uri(serviceRegistryOptions.ServiceAddress);
            // 创建Consul服务注册对象
            var registration = new AgentServiceRegistration()
            {
                ID = string.IsNullOrEmpty(serviceRegistryOptions.ServiceId) ? Guid.NewGuid().ToString() : serviceRegistryOptions.ServiceId,
                Name = serviceRegistryOptions.ServiceName,
                Address = uri.Host,
                Port = uri.Port,
                Tags = serviceRegistryOptions.ServiceTags,
                Check = new AgentServiceCheck
                {
                    // Consul 健康检查超时实践
                    Timeout = TimeSpan.FromSeconds(10),
                    // 服务停止5秒后注销服务
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    // Consul 健康检查地址 
                    HTTP = $"{uri.Scheme}://{uri.Host}:{uri.Port}{serviceRegistryOptions.HealthCheckAddress}",
                    // Consul 健康检查间隔时间
                    Interval = TimeSpan.FromSeconds(10)
                }
            };
            try
            {
                // 注册服务
                WriteResult result = consulClient.Agent.ServiceRegister(registration).Result;
                if (result != null && result.StatusCode.Equals(HttpStatusCode.OK))
                {
                    Console.WriteLine($"服务注册成功：{serviceRegistryOptions.ServiceAddress}");
                }
                else
                {
                    Console.WriteLine($"服务注册失败咯~~~：{serviceRegistryOptions.ServiceAddress}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Consul 注册服务发生异常：{ex.Message}");
                throw new FrameworkException(ex.Message);
            }
            // 关闭连接
            consulClient.Dispose();
        }
    }
}
