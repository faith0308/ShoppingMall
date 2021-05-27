﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 秒杀聚合服务 健康检查
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("HealthCheck")]
    [ApiController]
    public class HealthCheckController : Controller
    {
        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
