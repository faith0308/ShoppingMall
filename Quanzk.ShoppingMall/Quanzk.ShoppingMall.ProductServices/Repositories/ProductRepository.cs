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
    /// 商品仓储实现
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _productContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productContext"></param>
        public ProductRepository(ProductContext productContext)
        {
            _productContext = productContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public bool Create(Product product)
        {
            var flag = false;
            if (product != null)
            {
                _productContext.Products.Add(product);
                if (_productContext.SaveChanges() > 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool Delete(int Id)
        {
            var flag = false;
            if (Id > 0)
            {
                var entity = _productContext.Products.Find(Id);
                if (entity != null)
                {
                    _productContext.Products.Remove(entity);
                    if (_productContext.SaveChanges() > 0)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            var result = _productContext.Products.Find(id);
            return result ?? new Product();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            var result = _productContext.Products.ToList();
            return result ?? new List<Product>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ProductExists(int id)
        {
            return _productContext.Products.Any(p => p.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        public bool Update(Product product)
        {
            var falg = false;
            if (product != null && product.Id > 0)
            {
                var entity = _productContext.Products.Find(product.Id);
                if (entity != null)
                {
                    entity.ProductCode = product.ProductCode;
                    entity.ProductUrl = product.ProductUrl;
                    entity.ProductDescription = product.ProductDescription;
                    entity.ProductVirtualprice = product.ProductVirtualprice;
                    entity.ProductPrice = product.ProductPrice;
                    entity.ProductSold = product.ProductSold;
                    entity.ProductSort = product.ProductSort;
                    entity.ProductStock = product.ProductStock;
                    entity.ProductStatus = product.ProductStatus;
                    entity.ProductTitle = product.ProductTitle;
                    _productContext.Products.Update(entity);
                    if (_productContext.SaveChanges() > 0)
                    {
                        falg = true;
                    }
                }
            }
            return falg;
        }
    }
}
