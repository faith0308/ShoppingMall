using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Models
{
    /// <summary>
    /// 支付模型
    /// </summary>
    [Table("payments")]
    public class Payment
    {
        /// <summary>
        /// 支付编号
        /// </summary>
        [Key]
        public int Id { set; get; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public string PaymentPrice { set; get; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public string PaymentStatus { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public int OrderId { set; get; }
        /// <summary>
        /// 支付类型
        /// </summary>
        public int PaymentType { set; get; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentMethod { set; get; }
        /// <summary>
        /// 支付创建时间
        /// </summary>
        public DateTime Createtime { set; get; }
        /// <summary>
        /// 支付更新时间
        /// </summary>
        public DateTime Updatetime { set; get; }
        /// <summary>
        /// 支付备注
        /// </summary>
        public string PaymentRemark { set; get; }
        /// <summary>
        /// 支付url
        /// </summary>
        public string PaymentUrl { set; get; }
        /// <summary>
        /// 支付回调url
        /// </summary>
        public string PaymentReturnUrl { set; get; }
        /// <summary>
        /// 支付单号
        /// </summary>
        public string PaymentCode { set; get; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// 支付错误编号
        /// </summary>
        public string PaymentErrorNo { set; get; }
        /// <summary>
        /// 支付错误信息
        /// </summary>
        public string PaymentErrorInfo { set; get; } 
    }
}
