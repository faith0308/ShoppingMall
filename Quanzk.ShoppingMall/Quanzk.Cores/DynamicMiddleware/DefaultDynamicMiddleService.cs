using Quanzk.Cores.DynamicMiddleware.Urls;
using Quanzk.Cores.Exceptions;
using Quanzk.Cores.Middleware;
using Quanzk.Cores.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.DynamicMiddleware
{
    /// <summary>
    /// 动态中台服务，从注册中心动态获取服务
    /// </summary>
    public class DefaultDynamicMiddleService : IDynamicMiddleService
    {
        /// <summary>
        /// 中台组件
        /// </summary>
        private readonly IMiddleService httpMiddleService;
        /// <summary>
        /// 动态url组件
        /// </summary>
        private readonly IDynamicMiddleUrl dynamicMiddleUrl;

        public DefaultDynamicMiddleService(IMiddleService httpMiddleService, IDynamicMiddleUrl dynamicMiddleUrl)
        {
            this.httpMiddleService = httpMiddleService;
            this.dynamicMiddleUrl = dynamicMiddleUrl;
        }

        public void Delete(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Delete(url, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);
        }

        public dynamic DeleteDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Delete(url, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public IDictionary<string, object> Get(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Get(url + serviceLink, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.resultDic;
        }

        public T Get<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam) where T : new()
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Get(url + serviceLink, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return ConvertUtil.MiddleResultToObject<T>(middleResult);
        }

        public dynamic GetDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Get(url + serviceLink, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public IList<IDictionary<string, object>> GetList(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Get(url + serviceLink, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.resultList;
        }

        public IList<T> GetList<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam) where T : new()
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Get(url + serviceLink, reqParam);
            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return ConvertUtil.MiddleResultTList<T>(middleResult);
        }

        public void Post(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Post(url + serviceLink, reqParam);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);
        }

        public void Post(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> reqParams)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Post(url + serviceLink, reqParams);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);
        }

        public dynamic PostDynamic(string urlShcme, string serviceName, string serviceLink, dynamic reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Post(url + serviceLink, reqParam);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public void Put(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Put(url + serviceLink, reqParam);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);
        }

        public void Put(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> reqParams)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Put(url + serviceLink, reqParams);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);
        }

        public dynamic PutDynamic(string urlShcme, string serviceName, string serviceLink, dynamic reqParam)
        {
            // 构建中台请求Url
            var url = dynamicMiddleUrl.GetMiddleUrl(urlShcme, serviceName);
            // 执行请求
            MiddleResult middleResult = httpMiddleService.Put(url + serviceLink, reqParam);

            // 判断是否成功， 失败抛出原因
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        /// <summary>
        /// 判断请求是否成功
        /// </summary>
        /// <param name="middleResult"></param>
        private void IsSuccess(MiddleResult middleResult)
        {
            if (!middleResult.Code.Equals("0"))
                throw new FrameworkException(middleResult.Code, middleResult.MsgInfo);
        }
    }
}
