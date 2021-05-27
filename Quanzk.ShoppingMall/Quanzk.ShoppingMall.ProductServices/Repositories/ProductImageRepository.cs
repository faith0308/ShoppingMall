using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.ProductServices.Context;
using Quanzk.ShoppingMall.ProductServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Repositories
{
    /// <summary>
    /// 商品图片仓储实现
    /// </summary>
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ProductContext _productContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productContext"></param>
        public ProductImageRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public bool Create(ProductImage reqParam)
        {
            var falg = false;
            if (reqParam != null)
            {
                _productContext.ProductImages.Add(reqParam);
                var result = _productContext.SaveChanges();
                if (result > 0)
                {
                    falg = true;
                }
            }
            return falg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            var falg = false;
            if (Id > 0)
            {
                var entity = _productContext.ProductImages.Find(Id);
                if (entity != null)
                {
                    _productContext.ProductImages.Remove(entity);
                    var result = _productContext.SaveChanges();
                    if (result > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductImage GetProductImageById(int id)
        {
            var result = _productContext.ProductImages.Find(id);
            return result ?? new ProductImage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductImage> GetProductImages()
        {
            var result = _productContext.ProductImages.ToList();
            return result ?? new List<ProductImage>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productImage"></param>
        /// <returns></returns>
        public IEnumerable<ProductImage> GetProductImages(ProductImage productImage)
        {
            var result = _productContext.ProductImages.Where(p => p.ProductId == productImage.ProductId).ToList();
            return result ?? new List<ProductImage>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ProductImageExists(int id)
        {
            return _productContext.ProductImages.Any(p => p.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public bool Update(ProductImage reqParam)
        {
            var falg = false;
            if (reqParam != null)
            {
                var entity = _productContext.ProductImages.Find(reqParam.Id);
                if (entity != null)
                {
                    entity.ProductId = reqParam.ProductId;
                    entity.ImageSort = reqParam.ImageSort;
                    entity.ImageStatus = reqParam.ImageStatus;
                    entity.ImageUrl = reqParam.ImageUrl;
                    _productContext.ProductImages.Update(entity);
                    var result = _productContext.SaveChanges();
                    if (result > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }
    }
}
