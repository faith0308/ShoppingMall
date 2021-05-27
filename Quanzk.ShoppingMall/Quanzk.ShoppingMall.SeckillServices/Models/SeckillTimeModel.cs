using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Models
{
    /// <summary>
    /// 秒杀时间模型
    /// </summary>
    [Table("seckilltimemodels")]
    public class SeckillTimeModel
    {
        /// <summary>
        /// 秒杀时间编号
        /// </summary>
        [Key]
        public int Id { set; get; }
        /// <summary>
        /// 秒杀时间主题url
        /// </summary>
        public string TimeTitleUrl { set; get; }
        /// <summary>
        /// 秒杀日期(2021/5/1)
        /// </summary>
        public string SeckillDate { set; get; }
        /// <summary>
        /// 秒杀开始时间点(23:11)
        /// </summary>
        public string SeckillStarttime { set; get; }
        /// <summary>
        /// 秒杀结束时间点(23:59)
        /// </summary>
        public string SeckillEndtime { set; get; }
        /// <summary>
        /// 秒杀时间状态（0：启动，1：禁用）
        /// </summary>
        public int TimeStatus { set; get; }
    }
}
