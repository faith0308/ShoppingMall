using Quanzk.Cores.MicroClients.Attributes;
using Quanzk.ShoppingMall.SeckillAggregateServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Services
{
    /// <summary>
    /// 商品微服务客户端
    /// </summary>
    [MicroClient("https", "ProductServices")]
    public interface IProductClient
    {
        /// <summary>
        /// 查询所有商品信息
        /// </summary>
        /// <returns></returns>
        [GetPath("/Products")]
        public List<Product> GetProducts();

        /// <summary>
        /// 查询商品信息(根据Id)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [GetPath("/Products/{productId}")]
        public Product GetProduct(int productId);

        /// <summary>
        /// 扣减商品库存
        /// </summary>
        /// <param name="productId">商品Id</param>
        /// <param name="productCount">扣减数量</param>
        /// <returns></returns>
        [PutPath("/Products/{ProductId}/set-stock")]
        public bool ProductSetStock(int productId, int productCount);
    }
}
