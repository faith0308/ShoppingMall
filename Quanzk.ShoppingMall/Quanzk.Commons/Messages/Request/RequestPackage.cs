using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 请求包 
    /// </summary>
    public class RequestPackage : RequestBase
    {
    }

    /// <summary>
    /// 请求包 - 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RequestPackage<T> : RequestPackage
    { 
        /// <summary>
        /// 请求参数对象
        /// </summary>
        public T Query { get; set; }
    }

    #region 分页请求包
    /// <summary>
    /// 分页请求包
    /// </summary>
    public class PageRequestPackage : RequestPackage
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页显示数
        /// </summary>
        public int PageSize { get; set; }
    } 

    /// <summary>
    /// 分页请求包 - 泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class PageRequestPackage<T> : PageRequestPackage
    {
        /// <summary>
        /// 请求参数对象
        /// </summary>
        public T Query { get; set; }
    }

    #endregion
}
