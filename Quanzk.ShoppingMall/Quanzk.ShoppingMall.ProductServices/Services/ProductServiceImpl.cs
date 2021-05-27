using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.ProductServices.Models;
using Quanzk.ShoppingMall.ProductServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Services
{
    /// <summary>
    /// 商品服务实现
    /// </summary>
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productRepository"></param>
        public ProductServiceImpl(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public bool Delete(int Id)
        {
            return _productRepository.Delete(Id);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetProductById(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public bool ProductExists(int id)
        {
            return _productRepository.ProductExists(id);
        }

        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
