using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.ProductServices.Dto;
using Quanzk.ShoppingMall.ProductServices.Models;
using Quanzk.ShoppingMall.ProductServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.ProductServices.Controllers
{
    /// <summary>
    /// 产品服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<Product>> GetProducts()
        {
            var response = new ResponseListPackage<Product>();
            var products = _productService.GetProducts();
            if (products != null && products.Count() > 0)
            {
                response.Results = products;
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
        public ActionResult<ResponsePackage<Product>> GetProduct(int id)
        {
            var response = new ResponsePackage<Product>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var product = _productService.GetProductById(id);
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
        public ActionResult<ResponsePackage> Create(RequestPackage<Product> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null && !string.IsNullOrEmpty(reqParam.Query.ProductTitle))
            {
                if (_productService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<Product> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _productService.GetProductById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_productService.Update(request.Query))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// 扣减库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("EditProductStock")]
        public ActionResult<ResponsePackage> EditProductStock(RequestPackage<ProductInput> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.ProductId > 0)
            {
                // 查询商品
                var entity = _productService.GetProductById(request.Query.ProductId);
                if (entity != null && entity.Id > 0)
                {
                    // 判断商品是否有库存
                    if (entity.ProductStock > 0)
                    {
                        entity.ProductStock = entity.ProductStock - request.Query.ProductCount;

                        if (_productService.Update(entity))
                        {
                            response.Success(1, "扣减库存成功");
                        }
                    }
                    else
                    {
                        response.Error(-1, "商品库存不足");
                    }
                }
                else
                {
                    response.Error(-1, "商品不存在");
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
                var entity = _productService.GetProductById(id);
                if (entity != null && entity.Id > 0)
                {
                    if (_productService.Delete(id))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
