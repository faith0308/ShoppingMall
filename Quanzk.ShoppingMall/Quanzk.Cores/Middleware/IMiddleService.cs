using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Middleware
{
    /// <summary>
    /// 中台服务通信  接口规范 可以是Http、Grpc、消息队列等
    /// </summary>
    public interface IMiddleService
    {
        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">Url链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public MiddleResult Get(string url, IDictionary<string, object> reqParam);

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="url">Url链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public MiddleResult Post(string url, IDictionary<string, object> reqParam);

        /// <summary>
        /// Post 请求 (集合参数)
        /// </summary>
        /// <param name="url">url链接</param>
        /// <param name="reqParams">集合参数</param>
        /// <returns></returns>
        public MiddleResult Post(string url, IList<IDictionary<string, object>> reqParams);

        /// <summary>
        /// Post 请求,动态参数
        /// </summary>
        /// <param name="url">Url链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public MiddleResult PostDynamic(string url, dynamic reqParam);

        /// <summary>
        /// Delete 请求
        /// </summary>
        /// <param name="url">Url链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public MiddleResult Delete(string url, IDictionary<string, object> reqParam);

        /// <summary>
        /// Put 请求
        /// </summary>
        /// <param name="url">Url链接</param>
        /// <param name="reqParam">请求参数</param>
        /// <returns></returns>
        public MiddleResult Put(string url, IDictionary<string, object> reqParam);

        /// <summary>
        /// Put 请求 (集合参数)
        /// </summary>
        /// <param name="url">url链接</param>
        /// <param name="reqParams">集合参数</param>
        /// <returns></returns>
        public MiddleResult Put(string url, IList<IDictionary<string, object>> reqParams);

        /// <summary>
        /// Put 请求，动态参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public MiddleResult PutDynamic(string url, dynamic reqParam);
    }
}
