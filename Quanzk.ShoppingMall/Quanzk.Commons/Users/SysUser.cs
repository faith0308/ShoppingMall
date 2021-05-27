using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Users
{
    /// <summary>
    /// 系统用户(用于存储用户状态相关信息)
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; } 
    }
}
