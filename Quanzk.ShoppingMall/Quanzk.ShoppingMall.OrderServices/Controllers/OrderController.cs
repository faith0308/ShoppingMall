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
    /// 订单服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<Order>> GetOrders()
        {
            var response = new ResponseListPackage<Order>();
            var orders = _orderService.GetOrders();
            if (orders != null && orders.Count() > 0)
            {
                response.Results = orders;
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
        public ActionResult<ResponsePackage<Order>> GetOrder(int id)
        {
            var response = new ResponsePackage<Order>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var order = _orderService.GetOrderById(id);
            if (order != null)
            {
                response.Result = order;
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
        public ActionResult<ResponsePackage> Create(RequestPackage<Order> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                reqParam.Query.Createtime = DateTime.Now;
                if (_orderService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<Order> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _orderService.GetOrderById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_orderService.Update(request.Query))
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
                var entity = _orderService.GetOrderById(id);
                if (entity != null)
                {
                    if (_orderService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
