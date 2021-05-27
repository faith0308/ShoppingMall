using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Exceptions;
using Quanzk.Commons.Messages;
using Quanzk.ShoppingMall.UserServices.Models;
using Quanzk.ShoppingMall.UserServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    [Authorize] // 保护起来
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            //throw new BusinessException("111");
            return _userService.GetUsers().ToList();
        }

        /// <summary>
        /// 根据Id查询对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserById")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<bool> Create(User user)
        {
            return _userService.Create(user);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<bool> Edit(User user)
        {
            var result = false;
            if (user != null && user.Id > 0)
            {
                var entity = _userService.GetUserById(user.Id);
                if (entity != null && entity.Id > 0)
                {
                    result = _userService.Update(user);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<bool> Delete(int id)
        {
            var result = false;
            if (id > 0)
            {
                var entity = _userService.GetUserById(id);
                if (entity != null && entity.Id > 0)
                {
                    result = _userService.Delete(id);
                }
            }
            return result;
        }
    }
}
