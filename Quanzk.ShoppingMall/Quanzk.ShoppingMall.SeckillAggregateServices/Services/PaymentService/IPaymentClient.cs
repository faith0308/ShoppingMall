using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 支付微服务客户端
    /// </summary>
    [MicroClient("https", "PaymentServices")]
    public interface IPaymentClient
    {
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [PostPath("Payments")]
        public Payment Pay(Payment payment);
    }
}
