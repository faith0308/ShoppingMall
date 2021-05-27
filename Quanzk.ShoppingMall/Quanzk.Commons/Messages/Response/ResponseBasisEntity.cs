using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 请求响应基础对象
    /// </summary>
    public class ResponseBasisEntity
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = -1;

        /// <summary>
        /// 提示消息
        /// </summary>
        public string Msg { get; set; } = "操作失败";

        /// <summary>
        /// 签名字段 MD5（{Content}+Md5Key）
        /// </summary>
        public string Sign { get; set; }
    }
}
