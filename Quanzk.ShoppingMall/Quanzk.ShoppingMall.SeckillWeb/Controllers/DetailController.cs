using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillWeb.Controllers
{
    /// <summary>
    /// 秒杀商品详情控制器
    /// </summary>
    public class DetailController : Controller
    {
        /// <summary>
        /// 首页展示
        /// </summary>
        /// <param name="seckillId">秒杀编号</param>
        /// <param name="endtime">秒杀时间</param>
        /// <returns></returns>
        public IActionResult Index(int seckillId,string endtime)
        {
            ViewData.Add("seckillId", seckillId);
            ViewData.Add("endtime", endtime);
            return View();
        }
    }
}
