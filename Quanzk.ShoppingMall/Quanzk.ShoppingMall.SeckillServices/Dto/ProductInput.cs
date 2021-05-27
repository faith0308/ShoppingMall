using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Dto
{
    /// <summary>
    /// 秒杀商品值对象，主要接受客户端传过来的参数
    /// </summary>
    public class ProductInput
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { set; get; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProductCount { set; get; }
    }
}
