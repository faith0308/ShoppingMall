using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillWeb.Controllers
{
    /// <summary>
    /// 订单页面控制器
    /// </summary>
    public class OrderController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="ProductCount"></param>
        /// <param name="ProductPrice"></param>
        /// <param name="ProductUrl"></param>
        /// <param name="ProductTitle"></param>
        /// <returns></returns>
        public IActionResult Index(int ProductId, int ProductCount,
                            decimal ProductPrice, string ProductUrl,
                            string ProductTitle)
        {
            ViewData.Add("ProductId", ProductId);
            ViewData.Add("ProductCount", ProductCount);
            ViewData.Add("ProductPrice", ProductPrice);
            ViewData.Add("ProductUrl", ProductUrl);
            ViewData.Add("ProductTitle", ProductTitle);
            return View();
        }
    }
}
