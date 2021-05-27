﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 支付聚合控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
