using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.IOC.Extentions
{
    /// <summary>
    /// 批量依赖注入封装
    /// 规则 Quanzk.*.dll  约定大于配置
    /// </summary>
    public static class BatchDependencyInjectionExtentions
    {
        /// <summary>
        /// 批量依赖注入封装
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var type = typeof(IDependency);
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            // 查找目录下所有已Quanzk开头,以dll结尾的程序集
            var referencedAssemblies = Directory.GetFiles(path, "Quanzk.*.dll").Select(Assembly.LoadFrom).ToArray();
            var types = referencedAssemblies.SelectMany(p => p.DefinedTypes).Select(t => t.AsType()).Where(x => x != type && type.IsAssignableFrom(x)).ToArray();
            // 筛选出所有的实现类
            var implementTypes = types.Where(x => x.IsClass).ToArray();
            // 筛选出定义的业务接口
            var interfaceTypes = types.Where(x => x.IsInterface).ToArray();
            foreach (var implementType in implementTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.IsAssignableFrom(implementType));
                if (interfaceType != null)
                {
                    // 依赖注入对象
                    services.Add(new ServiceDescriptor(interfaceType, implementType, ServiceLifetime.Scoped));
                }
            }
            return services;
        }
    }
}
