using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware
{
    /// <summary>
    /// 动态 中台服务 接口 
    /// </summary>
    public interface IDynamicMiddleService
    {
        /// <summary>
        /// 获取字典集合对象
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public IList<IDictionary<string, object>> GetList(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// 获取字典非集合对象
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public IDictionary<string, object> Get(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// 获取动态对象
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public dynamic GetDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// 获取泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public IList<T> GetList<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam) where T : new();


        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public T Get<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
            where T : new();

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public void Post(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// Post请求，动态参数
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public dynamic PostDynamic(string urlShcme, string serviceName, string serviceLink, dynamic reqParam);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public void Post(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> reqParams);


        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public void Delete(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public dynamic DeleteDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public void Put(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam);

        /// <summary>
        /// Put请求，动态参数
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public dynamic PutDynamic(string urlShcme, string serviceName, string serviceLink, dynamic reqParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="urlShcme">url</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="serviceLink">服务链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public void Put(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> reqParams);
    }
}
