using Quanzk.Commons.Messages;
using Quanzk.Cores.IOC;
using Quanzk.ShoppingMall.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Services
{
    /// <summary>
    /// 商品服务接口
    /// </summary>
    public interface IProductService: IDependency
    {
        /// <summary>
        ///  获取商品列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Product> GetProducts();

        /// <summary>
        /// 根据产品id获取商品对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(int id);

        /// <summary>
        /// 新增商品
        /// </summary>
        /// <param name="product"></param>
        bool Create(Product product);

        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="product"></param>
        bool Update(Product product);

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="Id"></param>
        bool Delete(int Id);

        /// <summary>
        /// 根据id判断商品是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ProductExists(int id);
    }
}
