using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.SeckillServices.Models;
using Quanzk.ShoppingMall.SeckillServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillServices.Controllers
{
    /// <summary>
    /// 秒杀记录服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class SeckillRecordsController : Controller
    {
        private readonly ISeckillRecordService _seckillRecordService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seckillRecordService"></param>
        public SeckillRecordsController(ISeckillRecordService seckillRecordService)
        {
            _seckillRecordService = seckillRecordService;
        }

        /// <summary>
        /// 获取秒杀商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<SeckillRecord>> GetSeckillRecords()
        {
            var response = new ResponseListPackage<SeckillRecord>();
            var list = _seckillRecordService.GetSeckillRecords();
            if (list != null && list.Count() > 0)
            {
                response.Results = list;
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
        public ActionResult<ResponsePackage<SeckillRecord>> GetSeckillRecord(int id)
        {
            var response = new ResponsePackage<SeckillRecord>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var entity = _seckillRecordService.GetSeckillRecordById(id);
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
        public ActionResult<ResponsePackage> Create(RequestPackage<SeckillRecord> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_seckillRecordService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<SeckillRecord> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _seckillRecordService.GetSeckillRecordById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_seckillRecordService.Update(request.Query))
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
                var entity = _seckillRecordService.GetSeckillRecordById(id);
                if (entity != null && entity.Id > 0)
                {
                    if (_seckillRecordService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
