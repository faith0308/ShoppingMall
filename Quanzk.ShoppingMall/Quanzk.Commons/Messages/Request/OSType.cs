using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Messages
{
    public enum OSType
    {
        /// <summary>
        /// 其他
        /// </summary>
        None = 0,
        /// <summary>
        /// WEB
        /// </summary>
        WEB = 1,
        /// <summary>
        /// IOS
        /// </summary>
        IOS = 2,
        /// <summary>
        /// Android
        /// </summary>
        Android = 3,
        /// <summary>
        /// 微信公众号
        /// </summary>
        WeChat = 4,
        /// <summary>
        /// 微信小程序
        /// </summary>
        WeChatApplet = 5,
        /// <summary>
        /// Pad-平板端
        /// </summary>
        Pad = 6,
        /// <summary>
        /// windows桌面客户端
        /// </summary>
        WinDesktop = 7,
    }
}
