using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.SeckillServices.Dto;
using Quanzk.ShoppingMall.SeckillServices.Models;
using Quanzk.ShoppingMall.SeckillServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Controllers
{
    /// <summary>
    /// 秒杀服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class SeckillsController : Controller
    {
        private readonly ISeckillService _seckillService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seckillService"></param>
        public SeckillsController(ISeckillService seckillService)
        {
            _seckillService = seckillService;
        }

        /// <summary>
        /// 获取秒杀商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<Seckill>> GetSeckills()
        {
            var response = new ResponseListPackage<Seckill>();
            var lists = _seckillService.GetSeckills();
            if (lists != null && lists.Count() > 0)
            {
                response.Results = lists;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 根据时间查询
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponseListPackage<Seckill>> GetList(RequestPackage<Seckill> request)
        {
            var response = new ResponseListPackage<Seckill>();
            var lists = _seckillService.GetSeckills(request.Query);
            if (lists != null && lists.Count() > 0)
            {
                response.Results = lists;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 根据商品Id获取秒杀商品对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponsePackage<Seckill>> GetSeckill(int id)
        {
            var response = new ResponsePackage<Seckill>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var entity = _seckillService.GetSeckillById(id);
            if (entity != null)
            {
                response.Result = entity;
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
        public ActionResult<ResponsePackage> Create(RequestPackage<Seckill> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_seckillService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<Seckill> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _seckillService.GetSeckillById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_seckillService.Update(request.Query))
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
                var entity = _seckillService.GetSeckillById(request.Query.ProductId);
                if (entity != null && entity.Id > 0)
                {
                    // 判断商品是否有库存
                    if (entity.SeckillStock > 0)
                    {
                        entity.SeckillStock = entity.SeckillStock - request.Query.ProductCount;

                        if (_seckillService.Update(entity))
                        {
                            response.Success(1, "扣减秒杀商品库存成功");
                        }
                    }
                    else
                    {
                        response.Error(-1, "秒杀商品库存不足");
                    }
                }
                else
                {
                    response.Error(-1, "秒杀商品不存在");
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
                var entity = _seckillService.GetSeckillById(id);
                if (entity != null && entity.Id > 0)
                {
                    if (_seckillService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }

    }
}
