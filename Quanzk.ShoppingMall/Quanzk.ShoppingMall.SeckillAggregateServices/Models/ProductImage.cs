
namespace Quanzk.ShoppingMall.SeckillAggregateServices.Models
{
    /// <summary>
    /// 商品图片对象
    /// </summary>
    public class ProductImage
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int ImageSort { set; get; }

        /// <summary>
        /// 状态（1：启用，2：禁用）
        /// </summary>
        public string ImageStatus { set; get; }

        /// <summary>
        /// 图片url
        /// </summary>
        public string ImageUrl { set; get; } 
    }
}
