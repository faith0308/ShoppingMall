using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Exceptions
{
    /// <summary>
    /// 业务异常类
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 业务异常编号
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 业务异常信息
        /// </summary>
        public string MsgInfo { get; }

        /// <summary>
        /// 业务异常详细信息
        /// </summary>
        public IDictionary<string, object> Infos { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorNo"></param>
        /// <param name="errorInfo"></param>
        public BusinessException(string errorNo, string errorInfo) : base(errorInfo)
        {
            Code = errorNo;
            MsgInfo = errorInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorNo"></param>
        /// <param name="errorInfo"></param>
        /// <param name="e"></param>
        public BusinessException(string errorNo, string errorInfo, Exception e) : base(errorInfo, e)
        {
            Code = errorNo;
            MsgInfo = errorInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorInfo"></param>
        public BusinessException(string errorInfo) : base(errorInfo)
        {
            Code = "-1";
            MsgInfo = errorInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="e"></param>
        public BusinessException(string errorInfo, Exception e) : base(errorInfo, e)
        {
            Code = "-1";
            MsgInfo = errorInfo;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e"></param>
        public BusinessException(Exception e)
        {
            Code = "-1";
            MsgInfo = e.Message;
        }
    }
}
