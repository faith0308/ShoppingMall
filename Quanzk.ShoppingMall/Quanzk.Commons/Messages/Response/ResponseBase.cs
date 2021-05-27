using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 请求响应基类
    /// </summary>
    public abstract class ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseBase()
        {
            this.basisEntity = new ResponseBasisEntity();
        }

        /// <summary>
        /// 
        /// </summary>
        public ResponseBasisEntity basisEntity { get; private set; }

        /// <summary>
        /// 成功响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns>ResponseBase</returns>
        public abstract ResponseBase Success(int code, string msg);

        /// <summary>
        /// 失败响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public abstract ResponseBase Error(int code, string msg);
    }
}
