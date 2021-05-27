using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Quanzk.Commons.Messages;
using Quanzk.Cores.Exceptions;
using Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Requests;
using Quanzk.ShoppingMall.SeckillAggregateServices.Dtos.Responses;
using Quanzk.ShoppingMall.SeckillAggregateServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 用户聚合控制器
    /// </summary>
    [Produces("application/json")]
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserClient _userClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClient"></param>
        public UserController(IUserClient userClient)
        {
            _userClient = userClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginReq"></param>
        [HttpPost]
        public ActionResult<UserRes> Login(LoginReq loginReq)
        {
            /*实现步骤
             * 1、查询用户信息 
             * 2、判断用户信息是否存在
             * 3、将用户信息生成token进行存储
             * 4、将token信息存储到cookie或者session中
             * 5、返回成功信息和token
             * 6、对于token进行认证(也就是身份认证)
            */
            // 获取IdentityServer接口文档
            HttpClient client = new HttpClient();
            DiscoveryDocumentResponse discoveryDocument = client.GetDiscoveryDocumentAsync("https://localhost:5001").Result;
            if (discoveryDocument.IsError)
            {
                throw new FrameworkException($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
            }

            // 根据用户名和密码建立token
            TokenResponse tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client-password",
                ClientSecret = "secret",
                GrantType = "password",
                UserName = loginReq.UserName,
                Password = loginReq.Password
            }).Result;

            // 返回AccessToken
            if (tokenResponse.IsError)
            {
                throw new FrameworkException($"{tokenResponse.Error + "," + tokenResponse.Raw}");
            }

            UserInfoResponse userInfoResponse = client.GetUserInfoAsync(new UserInfoRequest()
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            }).Result;

            // 返回用户信息
            UserRes userRes = new UserRes()
            {
                UserId = userInfoResponse.Json.TryGetString("sub"),
                UserName = loginReq.UserName,
                AccessToken = tokenResponse.AccessToken,
                ExpiresIn = tokenResponse.ExpiresIn
            };
            return userRes;
        }
    }
}
