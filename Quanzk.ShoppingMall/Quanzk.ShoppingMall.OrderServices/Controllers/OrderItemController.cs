using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.OrderServices.Models;
using Quanzk.ShoppingMall.OrderServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.OrderServices.Controllers
{
    /// <summary>
    /// 订单项服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<OrderItem>> GetOrderItems()
        {
            var response = new ResponseListPackage<OrderItem>();
            var orderItems = _orderItemService.GetOrderItems();
            if (orderItems != null && orderItems.Count() > 0)
            {
                response.Results = orderItems;
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
        public ActionResult<ResponsePackage<OrderItem>> GetOrderItem(int id)
        {
            var response = new ResponsePackage<OrderItem>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var orderItem = _orderItemService.GetOrderItemById(id);
            if (orderItem != null)
            {
                response.Result = orderItem;
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
        public ActionResult<ResponsePackage> Create(RequestPackage<OrderItem> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_orderItemService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<OrderItem> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _orderItemService.GetOrderItemById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_orderItemService.Update(request.Query))
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
                var entity = _orderItemService.GetOrderItemById(id);
                if (entity != null)
                {
                    if (_orderItemService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
