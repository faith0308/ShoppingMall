using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quanzk.Cores.Registry
{
    /// <summary>
    /// 服务启动时自动注册
    /// </summary>
    public class ServiceRegistryIHostedService : IHostedService
    {
        private readonly IServiceRegistry serviceRegistry;

        public ServiceRegistryIHostedService(IServiceRegistry serviceRegistry)
        {
            this.serviceRegistry = serviceRegistry;
        }

        /// <summary>
        /// 启动时注册服务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => serviceRegistry.Register());
        }

        /// <summary>
        /// 服务停止时自动注销
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            serviceRegistry.Deregister();
            return Task.CompletedTask;
        }
    }
}
