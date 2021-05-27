using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 订单微服务客户端
    /// </summary>
    [MicroClient("https", "OrderServices")]
    public interface IOrderClient
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [PostPath("/Orders")]
        public Order CreateOrder(Order order);
    }
}
