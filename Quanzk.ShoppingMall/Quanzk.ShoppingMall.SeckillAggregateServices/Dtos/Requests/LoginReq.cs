using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Requests
{
    /// <summary>
    /// 登录表单
    /// </summary>
    public class LoginReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; } 
    }
}
