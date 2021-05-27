using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Models
{
    /// <summary>
    /// 秒杀记录模型
    /// </summary>
    [Table("seckillrecords")]
    public class SeckillRecord
    {
        /// <summary>
        /// 秒杀记录编号
        /// </summary>
        [Key]
        public int Id { set; get; }
        /// <summary>
        /// 秒杀记录创建时间
        /// </summary>
        public DateTime Createtime { set; get; }
        /// <summary>
        /// 秒杀记录更新时间
        /// </summary>
        public DateTime Updatetime { set; get; }
        /// <summary>
        /// 秒杀记录总价
        /// </summary>
        public decimal RecordTotalprice { set; get; }
        /// <summary>
        /// 秒杀活动编号
        /// </summary>
        public int SeckillId { set; get; }
        /// <summary>
        /// 秒杀数量
        /// </summary>
        public int SeckillNum { set; get; }
        /// <summary>
        /// 秒杀价格
        /// </summary>
        public decimal SeckillPrice { set; get; }
        /// <summary>
        /// 商品原价
        /// </summary>
        public decimal ProductPrice { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderSn { set; get; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { set; get; }
        /// <summary>
        /// 秒杀记录状态
        /// </summary>
        public int RecordStatus { set; get; }            
    }
}
