using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Requests
{
    /// <summary>
    /// 用户参数
    /// </summary>
    public class UserReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string UserQQ { set; get; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserPhone { set; get; }
    }
}
