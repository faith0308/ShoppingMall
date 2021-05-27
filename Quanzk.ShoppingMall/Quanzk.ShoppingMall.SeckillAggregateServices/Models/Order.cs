using System;
using System.Collections.Generic;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Models
{
    public class Order
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { set; get; }

        /// <summary>
        /// 订单标志
        /// </summary>
        public string OrderFlag { set; get; } 

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderSn { set; get; }

        /// <summary>
        /// 订单总价
        /// </summary>
        public string OrderTotalPrice { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Updatetime { set; get; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime Paytime { set; get; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime Sendtime { set; get; }

        /// <summary>
        /// 订单完成时间
        /// </summary>
        public DateTime Successtime { set; get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { set; get; }

        /// <summary>
        /// 订单名称
        /// </summary>
        public string OrderName { set; get; }

        /// <summary>
        /// 订单电话
        /// </summary>
        public string OrderTel { set; get; }

        /// <summary>
        /// 订单地址
        /// </summary>
        public string OrderAddress { set; get; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string OrderRemark { set; get; }

        /// <summary>
        /// 订单项
        /// </summary>
        public List<OrderItem> OrderItems { set; get; }
    }
}
