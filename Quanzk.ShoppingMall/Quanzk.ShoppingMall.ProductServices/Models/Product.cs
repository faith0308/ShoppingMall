using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Models
{
    /// <summary>
    /// 产品对象
    /// </summary>
    [Table("products")]
    public class Product
    {
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// 商品标题
        /// </summary>
        public string ProductTitle { set; get; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string ProductCode { set; get; }

        /// <summary>
        /// 商品主图
        /// </summary>
        public string ProductUrl { set; get; }

        /// <summary>
        /// 图文描述
        /// </summary>
        public string ProductDescription { set; get; }

        /// <summary>
        /// 商品虚拟价格
        /// </summary>
        public decimal ProductVirtualprice { set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal ProductPrice { set; get; }

        /// <summary>
        /// 商品序号
        /// </summary>
        public int ProductSort { set; get; }

        /// <summary>
        /// 已售件数
        /// </summary>
        public int ProductSold { set; get; }

        /// <summary>
        /// 商品库存
        /// </summary>
        public int ProductStock { set; get; }

        /// <summary>
        /// 商品状态
        /// </summary>
        public string ProductStatus { set; get; } 
    }
}
