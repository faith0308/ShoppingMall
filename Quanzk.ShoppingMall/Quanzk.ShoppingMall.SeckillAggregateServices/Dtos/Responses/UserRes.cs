using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Responses
{
    /// <summary>
    /// 用户登录成功返回的信息
    /// </summary>
    public class UserRes
    {
        /// <summary>
        /// 执行token(用户身份)
        /// </summary>
        public string AccessToken { set; get; }

        /// <summary>
        /// AccessToken过期时间
        /// </summary>
        public int ExpiresIn { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
