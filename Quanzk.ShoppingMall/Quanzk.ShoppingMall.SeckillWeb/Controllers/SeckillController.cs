using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Quanzk.ShoppingMall.SeckillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillWeb.Controllers
{
    /// <summary>
    /// 秒杀列表控制器
    /// </summary>
    public class SeckillController : Controller
    {
        private readonly IOptions<AppSetting> _appSetting;

        public SeckillController(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting;
        }
        public IActionResult Index()
        {
            ViewData.Add("RequestUrl", _appSetting.Value.RequestUrl);
            return View();
        }
    }
}
