using Microsoft.Extensions.DependencyModel;
using Quanzk.Cores.MicroClients.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients
{
    /// <summary>
    /// 程序集工具类
    /// </summary>
    public class AssemblyUtil
    {
        /// <summary>
        /// 获取项目程序集，排除所有的系统程序集(Microsoft.***、System.***等)、Nuget下载包
        /// </summary>
        /// <returns></returns>
        public static IList<Assembly> GetAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            // //排除所有的系统程序集、Nuget下载包
            var libs = deps.CompileLibraries.Where(p => !p.Serviceable && p.Type != "package");
            foreach (var lib in libs)
            {
                try
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据名称获取程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(string assemblyName)
        {
            return GetAssemblies().FirstOrDefault(p => p.FullName.Contains(assemblyName));
        }

        /// <summary>
        /// 获取项目程序集中所有type对象
        /// </summary>
        /// <returns></returns>
        public static IList<Type> GetAllTypes()
        {
            var list = new List<Type>();
            foreach (var assembly in GetAssemblies())
            {
                var typeInfos = assembly.DefinedTypes;
                foreach (var typeInfo in typeInfos)
                {
                    list.Add(typeInfo.AsType());
                }
            }
            return list;
        }

        /// <summary>
        /// 根据AssemblyName获取所有的类
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static IList<Type> GetTypesByAssembly(string assemblyName)
        {
            var list = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeInfos = assembly.DefinedTypes;
            foreach (var typeInfo in typeInfos)
            {
                list.Add(typeInfo.AsType());
            }

            return list;
        }

        /// <summary>
        /// 根据assemblyName获取包含MicroClient的程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static IList<Type> GetMicroClientTypesByAssembly(string assemblyName)
        {
            var list = new List<Type>();
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            var typeInfos = assembly.DefinedTypes;
            foreach (var typeInfo in typeInfos)
            {
                // 判断是否有特性
                var microClient = typeInfo.GetCustomAttribute<MicroClient>();
                if (microClient != null)
                {
                    list.Add(typeInfo.AsType());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取实现类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="baseInterfaceType"></param>
        /// <returns></returns>
        public static Type GetImplementType(string typeName, Type baseInterfaceType)
        {
            return GetAllTypes().FirstOrDefault(p =>
            {
                if (p.Name.Equals(typeName) && p.GetTypeInfo().GetInterfaces().Any(b => b.Name.Equals(baseInterfaceType.Name)))
                {
                    var typeInfo = p.GetTypeInfo();
                    return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
                }
                return false;
            });
        }
    }
}
