using Castle.DynamicProxy;
using Newtonsoft.Json;
using Quanzk.Cores.DynamicMiddleware;
using Quanzk.Cores.Exceptions;
using Quanzk.Cores.MicroClients.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.MicroClients
{
    /// <summary>
    /// 微服务客户端代理
    /// </summary>
    public class MicroClientProxy : IInterceptor
    {
        private readonly IDynamicMiddleService dynamicMiddleService;

        public MicroClientProxy(IDynamicMiddleService dynamicMiddleService)
        {
            this.dynamicMiddleService = dynamicMiddleService;
        }

        /// <summary>
        /// 客户端代理执行
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            // 获取接口方法
            MethodInfo methodInfo = invocation.Method;
            // 获取方法上特性
            IEnumerable<Attribute> attributes = methodInfo.GetCustomAttributes();
            // 获取Url
            Type type = invocation.Method.DeclaringType;
            MicroClient microClient = type.GetCustomAttribute<MicroClient>();
            if (microClient == null)
            {
                throw new FrameworkException($"MicroClient 特性不能为空");
            }
            // 转换成动态参数
            ProxyMethodParameter proxyMethodParameter = new ProxyMethodParameter(methodInfo.GetParameters(), invocation.Arguments);
            dynamic paramPairs = ArgumentsConvert(proxyMethodParameter);
            // 遍历
            foreach (var attribute in attributes)
            {
                // Get 请求
                if (attribute is GetPath getPath)
                {
                    // 路径变量替换
                    string path = PathParse(getPath.Path, paramPairs);
                    // Get请求
                    dynamic result = dynamicMiddleService.GetDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);
                    // 获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    // 赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                //Post 请求
                else if (attribute is PostPath postPath)
                {
                    // 路径变量替换
                    string path = PathParse(postPath.Path, paramPairs);
                    // 执行
                    dynamic result = dynamicMiddleService.PostDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);
                    // 获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    // 赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                // Put 请求
                else if (attribute is PutPath putPath)
                {
                    // 路径变量替换
                    string path = PathParse(putPath.Path, paramPairs);
                    // 执行
                    dynamic result = dynamicMiddleService.PutDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);
                    // 获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    // 赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                // Delete 请求
                else if (attribute is DeletePath deletePath)
                {
                    // 路径变量替换
                    string path = PathParse(deletePath.Path, paramPairs);
                    // 执行
                    dynamic result = dynamicMiddleService.DeleteDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);
                    // 获取返回值类型
                    Type returnType = methodInfo.ReturnType;
                    // 赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else
                {
                    throw new FrameworkException($"方法特性不存在");
                }
            }
        }

        /// <summary>
        /// 结果转换
        /// </summary>
        /// <param name="result"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        private dynamic ResultConvert(dynamic result, Type convertType)
        {
            // 判断是否为void
            if (convertType == typeof(void))
            {
                return null;
            }
            // 先序列化json
            string resultJson = JsonConvert.SerializeObject(result);
            // 再反序列成需要的对象
            dynamic returnResult = JsonConvert.DeserializeObject(resultJson, convertType);
            return returnResult;
        }

        private dynamic ArgumentsConvert(ProxyMethodParameter proxyMethodParameter)
        {
            // 动态参数
            dynamic dynamicParams = new Dictionary<string, object>();
            // 多个参数情况包装成字典
            IDictionary<string, object> paramPairs = new Dictionary<string, object>();

            foreach (var parameterInfo in proxyMethodParameter.parameterInfos)
            {
                object parameterValue = proxyMethodParameter.Arguments[parameterInfo.Position];
                Type parameterType = parameterInfo.ParameterType;

                // 是否只有一个参数
                if (proxyMethodParameter.Arguments.Length == 1)
                {
                    // 如果是值类型
                    if (parameterType.IsValueType)
                    {
                        PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                        if (pathVariable != null)
                        {
                            // 设置路径变量名称
                            paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        }
                        else
                        {
                            paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        }
                        // 设置为动态返回
                        dynamicParams = paramPairs;
                    }
                    else
                    {
                        // 如果是引用类型，直接动态返回
                        dynamicParams = parameterValue;
                    }
                }
                else
                {
                    // 判断是否有两个以上(全部用字典组装起来)
                    PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                    if (pathVariable != null)
                    {
                        // 设置路径变量名称
                        paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    }
                    else
                    {
                        paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    }
                    // 设置为动态返回
                    dynamicParams = paramPairs;
                }
            }
            return dynamicParams;
        }

        /// <summary>
        /// 路径转换
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="paramPairs">参数</param>
        /// <returns></returns>
        private string PathParse(string path, dynamic paramPairs)
        {
            // 判断为字典进行路径解析
            if (paramPairs is IDictionary<string, object>)
            {
                // 路径前缀
                string pathPrefi = "{";
                // 路径后缀
                string pathSuffix = "}";
                foreach (var key in paramPairs.Keys)
                {
                    string pathvariable = pathPrefi + key + pathSuffix;
                    if (path.Contains(pathvariable))
                    {
                        path = path.Replace(pathvariable, Convert.ToString(paramPairs[key]));
                    }
                }
            }
            return path;
        }
    }

    /// <summary>
    /// 代理方法参数对象
    /// </summary>
    public class ProxyMethodParameter
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public ParameterInfo[] parameterInfos { get; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object[] Arguments { get; }

        public ProxyMethodParameter(ParameterInfo[] parameterInfos, object[] arguments)
        {
            this.parameterInfos = parameterInfos;
            this.Arguments = arguments;
        }
    }
}
