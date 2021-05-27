using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillWeb.Controllers
{
    /// <summary>
    /// 商品页面控制器
    /// </summary>
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
