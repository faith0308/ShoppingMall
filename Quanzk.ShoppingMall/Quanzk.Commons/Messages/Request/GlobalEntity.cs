using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    /// <summary>
    /// 全局请求对象
    /// </summary>
    public class GlobalEntity
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        ///  OS类型（OS类型：0-其他 1-Web，2-IOS 3-Android 4-微信公众号 5-微信小程序 6-pad平板端 7- windows桌面客户端）
        /// </summary>
        public OSType OS { get; set; }

        /// <summary>
        /// 签名字段 MD5（{Content}+Md5Key）
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// 用户令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 时间戳(精确到秒)
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// IMEI号
        /// </summary>
        public string IMEI { get; set; }
    }
}
