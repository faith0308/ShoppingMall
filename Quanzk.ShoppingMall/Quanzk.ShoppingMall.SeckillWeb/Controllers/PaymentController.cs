using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillWeb.Controllers
{
    /// <summary>
    /// 支付页面控制器
    /// </summary>
    public class PaymentController : Controller
    {
        public IActionResult Index(int OrderId, string OrderSn, decimal OrderTotalPrice, int ProductId, 
            string ProductName, int UserId)
        {
            ViewData.Add("OrderId", OrderId);
            ViewData.Add("OrderSn", OrderSn);
            ViewData.Add("OrderTotalPrice", OrderTotalPrice);
            ViewData.Add("ProductId", ProductId);
            ViewData.Add("ProductName", ProductName);
            ViewData.Add("UserId", UserId);
            return View();
        }
    }
}
