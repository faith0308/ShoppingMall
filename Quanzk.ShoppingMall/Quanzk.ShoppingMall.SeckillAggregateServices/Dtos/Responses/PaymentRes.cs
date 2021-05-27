using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Responses
{
    /// <summary>
    /// 支付Dto
    /// </summary>
    public class PaymentRes
    {
        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderSn { set; get; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderTotalPrice { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { set; get; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { set; get; }
    }
}
