using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Responses
{
    /// <summary>
    /// 秒杀活动Dto
    /// </summary>
    public class SeckillRes
    {
        /// <summary>
        /// 秒杀编号
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 秒杀类型
        /// </summary>
        public int SeckillType { set; get; }
        /// <summary>
        /// 秒杀名称
        /// </summary>
        public string SeckillName { set; get; }
        /// <summary>
        /// 秒杀URL
        /// </summary>
        public string SeckillUrl { set; get; }
        /// <summary>
        /// 秒杀价格
        /// </summary>
        public decimal SeckillPrice { set; get; }
        /// <summary>
        /// 秒杀库存
        /// </summary>
        public int SeckillStock { set; get; }
        /// <summary>
        /// 秒杀数量(已经秒杀的数量)
        /// </summary>
        public int SeckillSum { set; get; } 
        /// <summary>
        /// 秒杀百分比
        /// </summary>
        public string SeckillPercent { set; get; }
        /// <summary>
        /// 秒杀时间编号
        /// </summary>
        public int TimeId { set; get; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }
        /// <summary>
        /// 秒杀限制数量
        /// </summary>
        public int SeckillLimit { set; get; }
        /// <summary>
        /// 秒杀描述
        /// </summary>
        public string SeckillDescription { set; get; }
        /// <summary>
        /// 秒杀是否结束
        /// </summary>
        public int SeckillIstop { set; get; }
        /// <summary>
        /// 秒杀状态
        /// </summary>
        public int SeckillStatus { set; get; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ProductPrice { set; get; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string ProductDescription { set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductTitle { set; get; }
        /// <summary>
        /// 商品URL
        /// </summary>
        public string ProductUrl { set; get; }
    }
}
