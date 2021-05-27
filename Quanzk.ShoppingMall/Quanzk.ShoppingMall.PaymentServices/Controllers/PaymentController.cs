using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.PaymentServices.Models;
using Quanzk.ShoppingMall.PaymentServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.PaymentServices.Controllers
{
    /// <summary>
    /// 支付服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        /// <summary>
        /// 获取所有商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ResponseListPackage<Payment>> GetProducts()
        {
            var response = new ResponseListPackage<Payment>();
            var payments = _paymentService.GetPayments();
            if (payments != null && payments.Count() > 0)
            {
                response.Results = payments;
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
        public ActionResult<ResponsePackage<Payment>> GetProduct(int id)
        {
            var response = new ResponsePackage<Payment>();
            if (id <= 0)
            {
                response.Error();
                return response;
            }
            var payment = _paymentService.GetPaymentById(id);
            if (payment != null)
            {
                response.Result = payment;
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
        public ActionResult<ResponsePackage> Create(RequestPackage<Payment> reqParam)
        {
            var response = new ResponsePackage();
            if (reqParam != null && reqParam.Query != null)
            {
                if (_paymentService.Create(reqParam.Query))
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
        public ActionResult<ResponsePackage> Edit(RequestPackage<Payment> request)
        {
            var response = new ResponsePackage();
            if (request != null && request.Query != null && request.Query.Id > 0)
            {
                var entity = _paymentService.GetPaymentById(request.Query.Id);
                if (entity != null && entity.Id > 0)
                {
                    if (_paymentService.Update(request.Query))
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
                var entity = _paymentService.GetPaymentById(id);
                if (entity != null)
                {
                    if (_paymentService.Delete(entity))
                    {
                        response.Success();
                    }
                }
            }
            return response;
        }
    }
}
