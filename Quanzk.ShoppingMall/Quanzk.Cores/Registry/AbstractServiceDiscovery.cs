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

namespace Quanzk.Cores.Registry
{
    /// <summary>
    /// 抽象服务发现 (主要是缓存功能)
    /// </summary>
    public abstract class AbstractServiceDiscovery : IServiceDiscovery
    {
        /// <summary>
        /// 服务节点 字典缓存
        /// </summary>
        private readonly Dictionary<string, List<ServiceNode>> cacheConsulResult = new Dictionary<string, List<ServiceNode>>();

        protected readonly ServiceDiscoveryOptions serviceDiscoveryOptions;

        public AbstractServiceDiscovery(IOptions<ServiceDiscoveryOptions> options)
        {
            this.serviceDiscoveryOptions = options.Value;
            // 创建 Consul 客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                // 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscoveryOptions.DiscoveryAddress);
            });

            // 先查询 Consul 服务
            var queryResult = consulClient.Catalog.Services().Result;
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameworkException($"Consul 连接失败：{queryResult.StatusCode}");
            }

            // 获取服务下的所有实例
            foreach (var item in queryResult.Response)
            {
                QueryResult<CatalogService[]> result = consulClient.Catalog.Service(item.Key).Result;
                if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw new FrameworkException($" Consul 连接失败:{queryResult.StatusCode}");
                }
                var list = new List<ServiceNode>();
                foreach (var service in result.Response)
                {
                    list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
                }
                cacheConsulResult.Add(item.Key, list);
            }
        }

        public List<ServiceNode> Discovery(string serviceName)
        {
            // 如果已存在，就从缓存中取 Consul 服务结果
            if (cacheConsulResult.ContainsKey(serviceName))
            {
                return cacheConsulResult[serviceName];
            }
            else
            {
                // 如果不存在，就从远程服务取 服务结果
                CatalogService[] queryResult = RemoteDiscovery(serviceName);
                var list = new List<ServiceNode>();
                foreach (var service in queryResult)
                {
                    list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
                }

                // 将结果添加到缓存
                cacheConsulResult.Add(serviceName, list);
                return list;
            }
        }

        /// <summary>
        /// 远程服务发现
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        protected abstract CatalogService[] RemoteDiscovery(string serviceName);
    }
}
