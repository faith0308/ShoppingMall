﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients
{
    /// <summary>
    /// 微服务客户端实例集合
    /// </summary>
    public class MicroClientList
    {
        public readonly MicroClientProxyFactory microClientProxyFactory;

        public MicroClientList(MicroClientProxyFactory microClientProxyFactory)
        {
            this.microClientProxyFactory = microClientProxyFactory;
        }

        /// <summary>
        /// 获取所有客户端实例
        /// </summary>
        /// <param name="assmalyName"></param>
        /// <returns></returns>
        public IDictionary<Type, object> GetClients(string assmalyName)
        {
            // 加载所有MicrolClient特性类型
            IList<Type> types = AssemblyUtil.GetMicroClientTypesByAssembly(assmalyName);
            // 创建所有类型实例
            IDictionary<Type, object> keyValuePairs = new Dictionary<Type, object>();
            foreach (var type in types)
            {
                object value = microClientProxyFactory.CreateMicroClientProxy(type);
                keyValuePairs.Add(type, value);
            }
            return keyValuePairs;
        }
    }
}
