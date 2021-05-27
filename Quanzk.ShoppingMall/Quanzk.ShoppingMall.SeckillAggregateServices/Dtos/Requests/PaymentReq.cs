using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Requests
{
    /// <summary>
    /// 支付参数模型
    /// </summary>
    public class PaymentReq
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { set; get; }
        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { set; get; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PaymentType { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderSn { set; get; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal OrderTotalPrice { set; get; } 
    }
}
