using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 商品图片微服务客户端
    /// </summary>
    [MicroClient("https", "ProductServices")]
    public interface IProductImageClient
    {
        /// <summary>
        /// 根据商品Id查询所有商品图片信息
        /// </summary>
        /// <param name="productId">商品Id</param>
        /// <returns></returns>
        [GetPath("/Products/{productId}/ProductImages")]
        public List<ProductImage> GetProductImages(int productId);
    }
}
