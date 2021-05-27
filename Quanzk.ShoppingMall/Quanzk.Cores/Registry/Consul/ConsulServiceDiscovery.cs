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
    ///  Consul 服务发现 实现
    /// </summary>
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly ServiceDiscoveryOptions serviceDiscoveryOptions;

        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryOptions> serviceDiscoveryOptions)
        {
            this.serviceDiscoveryOptions = serviceDiscoveryOptions.Value;
        }


        public List<ServiceNode> Discovery(string serviceName)
        {
            // 从远程服务器取服务
            CatalogService[] queryResult = RemoteDiscovery(serviceName);
            var list = new List<ServiceNode>();
            foreach (var service in queryResult)
            {
                list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
            }
            return list;
        }

        private CatalogService[] RemoteDiscovery(string serviceName)
        {
            // 创建consul 客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                // 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscoveryOptions.DiscoveryAddress);
            });

            // Consul 查询服务，根据具体的服务名称查询
            var queryResult = consulClient.Catalog.Service(serviceName).Result;
            // 判断请求是否失败
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameworkException($"consul连接失败：{queryResult.StatusCode}");
            }

            return queryResult.Response;
        }
    }
}
