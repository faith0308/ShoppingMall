using Castle.DynamicProxy;
using Quanzk.Cores.DynamicMiddleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients
{
    /// <summary>
    /// 创建微服务客户端代理
    /// </summary>
    public class MicroClientProxyFactory
    {
        private readonly IDynamicMiddleService dynamicMiddleService;

        public MicroClientProxyFactory(IDynamicMiddleService dynamicMiddleService)
        {
            this.dynamicMiddleService = dynamicMiddleService;
        }

        /// <summary>
        /// 创建接口代理类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateMicroClientProxy(Type type)
        {
            // 类和接口代理对象
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            // 创建代理对象，拦截对接口的成员的调用 
            object obj = proxyGenerator.CreateInterfaceProxyWithoutTarget(type, new MicroClientProxy(dynamicMiddleService));
            return obj;
        }
    }
}
