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
    /// 商品图片服务实现
    /// </summary>
    public class ProductImageServiceImpl : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageServiceImpl(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public bool Create(ProductImage reqParam)
        {
            return _productImageRepository.Create(reqParam);
        }

        public bool Delete(int Id)
        {
            return _productImageRepository.Delete(Id);
        }

        public ProductImage GetProductImageById(int id)
        {
            return _productImageRepository.GetProductImageById(id);
        }

        public IEnumerable<ProductImage> GetProductImages()
        {
            return _productImageRepository.GetProductImages();
        }

        public IEnumerable<ProductImage> GetProductImages(ProductImage productImage)
        {
            return _productImageRepository.GetProductImages(productImage);
        }

        public bool ProductImageExists(int id)
        {
            return _productImageRepository.ProductImageExists(id);
        }

        public bool Update(ProductImage reqParam)
        {
            return _productImageRepository.Update(reqParam);
        }
    }
}
