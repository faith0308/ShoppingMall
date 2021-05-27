using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Models
{
    /// <summary>
    /// 订单项(记录购买商品信息)
    /// </summary>
    [Table("orderitems")]
    public class OrderItem
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { set; get; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderSn { set; get; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// 商品主图
        /// </summary>
        public string ProductUrl { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { set; get; }
        /// <summary>
        /// 订单项单价
        /// </summary>
        public decimal ItemPrice { set; get; }
        /// <summary>
        /// 订单项数量
        /// </summary>
        public int ItemCount { set; get; }
        /// <summary>
        /// 订单项总价
        /// </summary>
        public decimal ItemTotalPrice { set; get; } 
    }
}
