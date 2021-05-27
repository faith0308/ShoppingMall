using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.TestWeb.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PrivacyController : Controller
    {
        private readonly ILogger<PrivacyController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public PrivacyController(ILogger<PrivacyController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
