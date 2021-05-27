using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.ProductServices.Models;
using Quanzk.ShoppingMall.ProductServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Controllers
{
    /// <summary>
    /// 商品图片服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ProductImagesController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<ProductImage>> GetProducts()
        {
            var response = new ResponseListPackage<ProductImage>();
            var productImages = _productImageService.GetProductImages();
            if (productImages != null && productImages.Count() > 0)
            {
                response.Results = productImages;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 根据商品Id获取商品对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponsePackage<ProductImage>> GetProduct(int id)
        {
            var response = new ResponsePackage<ProductImage>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var product = _productImageService.GetProductImageById(id);
            if (product != null)
            {
                response.Result = product;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponsePackage> Create(RequestPackage<ProductImage> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_productImageService.Create(reqParam.Query))
                {
                    response.Success();
                }
            }
            return response;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ResponsePackage> Edit(RequestPackage<ProductImage> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _productImageService.GetProductImageById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_productImageService.Update(request.Query))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<ResponsePackage> Delete(int id)
        {
            var response = new ResponsePackage();
            if (id > 0)
            {
                var entity = _productImageService.GetProductImageById(id);
                if (entity != null && entity.Id > 0)
                {
                    if (_productImageService.Delete(id))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
