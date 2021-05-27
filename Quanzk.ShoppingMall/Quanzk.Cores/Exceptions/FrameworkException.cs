using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Cores.Exceptions
{
    /// <summary>
    /// 框架异常处理类
    /// </summary>
    public class FrameworkException : Exception
    {
        /// <summary>
        /// 成功状态码 默认0成功，-1失败
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string MsgInfo { get; set; }

        /// <summary>
        /// 字典类型结果信息
        /// </summary>
        public IDictionary<string, object> MsgInfos { get; set; }

        public FrameworkException(string code, string msgInfo)
        {
            this.Code = code;
            this.MsgInfo = msgInfo;
        }

        public FrameworkException(string code, string msgInfo, Exception ex) : base(msgInfo, ex)
        {
            this.Code = code;
            this.MsgInfo = msgInfo;
        }

        public FrameworkException(string msgInfo) : base(msgInfo)
        {
            this.Code = "-1";
            this.MsgInfo = msgInfo;
        }

        public FrameworkException(string msgInfo, Exception ex) : base(msgInfo, ex)
        {
            this.Code = "-1";
            this.MsgInfo = msgInfo;
        }

        public FrameworkException(Exception e)
        {
            this.Code = "-1";
            this.MsgInfo = e.Message;
        }
    }
}
