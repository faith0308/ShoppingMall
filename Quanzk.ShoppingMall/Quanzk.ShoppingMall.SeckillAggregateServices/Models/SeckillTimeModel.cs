
namespace Quanzk.ShoppingMall.SeckillAggregateServices.Models
{
    /// <summary>
    /// 秒杀时间模型
    /// </summary>
    public class SeckillTimeModel
    {
        /// <summary>
        /// 秒杀时间编号
        /// </summary>
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
