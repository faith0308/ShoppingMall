using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 用户微服务客户端
    /// </summary>
    [MicroClient("https", "UserServices")]
    public interface IUserClient
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [PostPath("/Users")]
        public User AddUsers(User user);
    }
}
