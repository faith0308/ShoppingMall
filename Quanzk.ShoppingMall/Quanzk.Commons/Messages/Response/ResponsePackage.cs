using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 默认响应数据包
    /// </summary>
    public class ResponsePackage : ResponseBase
    {
        /// <summary>
        /// 失败响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public override ResponsePackage Error(int code = -1, string msg = "操作失败")
        {
            this.basisEntity.Code = code;
            this.basisEntity.Msg = msg;
            return this;
        }

        /// <summary>
        /// 成功响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public override ResponsePackage Success(int code = 0, string msg = "操作成功")
        {
            this.basisEntity.Code = code;
            this.basisEntity.Msg = msg;
            return this;
        }
    }

    /// <summary>
    /// 请求响应数据包 (响应状态+返回实体对象)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponsePackage<T> : ResponsePackage
    {
        public T Result { get; set; }
    }

    /// <summary>
    /// 请求响应数据包 (响应状态+返回实体对象集合)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponseListPackage<T> : ResponsePackage
    {
        public ResponseListPackage()
        {
            this.Results = new List<T>();
        }

        /// <summary>
        /// 响应对象集合
        /// </summary>
        public IEnumerable<T> Results { get; set; }
    }

    /// <summary>
    /// 分页请求响应数据包 (响应状态+返回实体对象集合)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ResponsePagePackage<T> : ResponsePackage
    {
        public ResponsePagePackage()
        {
            this.Results = new List<T>();
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 响应对象集合
        /// </summary>
        public IEnumerable<T> Results { get; set; }
    }

    /// <summary>
    /// 请求响应数据包 (响应状态+动态集合)
    /// </summary>
    public sealed class DynamicResponsePackage : ResponseBase
    {
        public dynamic Result { get; set; }

        /// <summary>
        /// 失败响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public override ResponseBase Error(int code = -1, string msg = "操作失败")
        {
            this.basisEntity.Code = code;
            this.basisEntity.Msg = msg;
            this.Result = Result;
            return this;
        }

        /// <summary>
        /// 成功响应
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public override ResponseBase Success(int code = 0, string msg = "操作成功")
        {
            this.basisEntity.Code = code;
            this.basisEntity.Msg = msg;
            this.Result = Result;
            return this;
        }
    }
}
