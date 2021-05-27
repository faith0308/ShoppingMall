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
    /// 商品图片服务接口
    /// </summary>
    public interface IProductImageService : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductImage> GetProductImages();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productImage"></param>
        /// <returns></returns>
        IEnumerable<ProductImage> GetProductImages(ProductImage productImage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductImage GetProductImageById(int id);

        /// <summary>
        /// 新增商品图片
        /// </summary>
        /// <param name="reqParam"></param>
        bool Create(ProductImage reqParam);

        /// <summary>
        /// 修改商品图片
        /// </summary>
        /// <param name="reqParam"></param>
        bool Update(ProductImage reqParam);

        /// <summary>
        /// 删除商品图片
        /// </summary>
        /// <param name="Id"></param>
        bool Delete(int Id);

        /// <summary>
        /// 根据id判断商品图片是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ProductImageExists(int id);
    }
}
