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
    /// 秒杀时间服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class SeckillTimeModelsController : Controller
    {
        private readonly ISeckillTimeModelService _seckillTimeModelService;
        private readonly ISeckillService _seckillService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seckillTimeModelService"></param>
        /// <param name="seckillService"></param>
        public SeckillTimeModelsController(
            ISeckillTimeModelService seckillTimeModelService,
            ISeckillService seckillService)
        {
            _seckillTimeModelService = seckillTimeModelService;
            _seckillService = seckillService;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<SeckillTimeModel>> GetSeckillRecords()
        {
            var response = new ResponseListPackage<SeckillTimeModel>();
            var list = _seckillTimeModelService.GetSeckillTimeModels();
            if (list != null && list.Count() > 0)
            {
                response.Results = list;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 根据Id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponsePackage<SeckillTimeModel>> GetSeckillRecord(int id)
        {
            var response = new ResponsePackage<SeckillTimeModel>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var entity = _seckillTimeModelService.GetSeckillTimeModelById(id);
            if (entity != null)
            {
                response.Result = entity;
                response.Success();
            }
            return response;
        }

        /// <summary>
        /// 根据时间编号获取秒杀活动
        /// </summary>
        /// <param name="timeId"></param>
        /// <returns></returns>
        [HttpGet("GetSeckills")]
        public ActionResult<IEnumerable<Seckill>> GetSeckills(int timeId)
        {
            var list = _seckillService.GetSeckills(new Seckill
            {
                TimeId = timeId
            }).ToList();
            return list ?? new List<Seckill>();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="reqParam"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ResponsePackage> Create(RequestPackage<SeckillTimeModel> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_seckillTimeModelService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<SeckillTimeModel> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _seckillTimeModelService.GetSeckillTimeModelById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    entity.SeckillDate = request.Query.SeckillDate;
                    entity.SeckillEndtime = request.Query.SeckillEndtime;
                    entity.SeckillStarttime = request.Query.SeckillStarttime;
                    entity.TimeStatus = request.Query.TimeStatus;
                    entity.TimeTitleUrl = request.Query.TimeTitleUrl;
                    if (_seckillTimeModelService.Update(entity))
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
                var entity = _seckillTimeModelService.GetSeckillTimeModelById(id);
                if (entity != null && entity.Id > 0)
                {
                    if (_seckillTimeModelService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
