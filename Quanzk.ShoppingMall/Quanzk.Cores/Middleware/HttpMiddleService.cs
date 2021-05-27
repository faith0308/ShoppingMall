using Newtonsoft.Json;
using Quanzk.Cores.Exceptions;
using Quanzk.Cores.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Middleware
{
    /// <summary>
    /// Http 中台请求结果
    /// </summary>
    public class HttpMiddleService : IMiddleService
    {
        // 这里使用的是HttpClient连接池，如果要用Grpc、消息队列等进行通讯，将此处改为其它通信即可
        private IHttpClientFactory httpClientFactory;
        private const string HttpConst = "Micro";

        public HttpMiddleService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public MiddleResult Delete(string url, IDictionary<string, object> reqParam)
        {
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            var httpResponseMessage = httpClient.DeleteAsync(url).Result;
            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Get(string url, IDictionary<string, object> reqParam)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 参数转换成Url方式
            var urlParam = HttpParamUtil.DicToHttpUrlParam(reqParam);
            // 执行请求，获取返回值
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(url + urlParam).Result;
            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string url, IDictionary<string, object> reqParam)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParam), Encoding.UTF8, "application/json");
            // 执行Post 请求，获取返回值
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string url, IList<IDictionary<string, object>> reqParams)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParams), Encoding.UTF8, "application/json");
            // 执行Post 请求，获取返回值
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult PostDynamic(string url, dynamic reqParam)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParam), Encoding.UTF8, "application/json");
            // 执行Post 请求，获取返回值
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(url, httpContent).Result;
            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string url, IDictionary<string, object> reqParam)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParam), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(url, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string url, IList<IDictionary<string, object>> reqParams)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParams), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(url, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult PutDynamic(string url, dynamic reqParam)
        {
            // 获取 httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);
            // 转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(reqParam), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(url, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        /// <summary>
        /// 获取http请求响应的 MiddleResult信息
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        private MiddleResult GetMiddleResult(HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.OK) || httpResponseMessage.StatusCode.Equals(HttpStatusCode.Created) || httpResponseMessage.StatusCode.Equals(HttpStatusCode.Accepted))
            {
                var httpJsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                return MiddleResult.JsonToMiddleResult(httpJsonString);
            }
            else
            {
                throw new FrameworkException($"{HttpConst} 服务调用错误：{httpResponseMessage.Content.ReadAsStringAsync()}");
            }
        }

    }
}
