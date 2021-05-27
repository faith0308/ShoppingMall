using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quanzk.Cores.Exceptions;
using Quanzk.ShoppingMall.Testb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.Testb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            #region  token模式
            {
                //var accessToken = await GetAccessToken();
                //var result = await UseAccessToken(accessToken);
                ////return new ContentResult { Content = result };
                //ViewData.Add("Json", result);
            }
            #endregion

            #region openid connect 模式
            {
                // 获取token(id_token , access_token ,refresh_token)
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                Console.WriteLine($"accessToken:{accessToken}");
                HttpClient client = new HttpClient();
                client.SetBearerToken(accessToken);

                // 使用token
                var result = await client.GetStringAsync("https://localhost:5001/api/v1/Users/GetUsers");

                ViewData.Add("Json", result);
            }
            #endregion
            return View();
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        private static async Task<string> GetAccessToken()
        {
            HttpClient client = new HttpClient();
            DiscoveryDocumentResponse disco = await client.GetDiscoveryDocumentAsync("https://localhost:5014");
            if (disco.IsError)
            {
                Console.WriteLine($"DiscoveryDocumentResponse Error:{disco.Error}");
            }
            // 1.通过客户端获取AccessToken
            //TokenResponse tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,// 生成accessToken中心地址
            //    ClientId = "client",// 客户端编号
            //    ClientSecret = "secret",// 客户端密码
            //    Scope = "UserServices"// 客户端需要访问的API
            //    //UserName="faith",
            //    //Password="123456"
            //});

            // 2.通过客户端用户密码获取AccessToken
            //TokenResponse tokenResponse = await client.RequestPasswordTokenAsync(
            //    new PasswordTokenRequest
            //    {
            //        Address = disco.TokenEndpoint,// 生成accessToken中心地址
            //        ClientId = "client-password",// 客户端编号
            //        ClientSecret = "secret",// 客户端密码
            //        Scope = "UserServices",// 客户端需要访问的API
            //        UserName = "bob",
            //        Password = "123456"
            //    });

            // 3.客户端授权码认证
            TokenResponse tokenResponse = await client.RequestAuthorizationCodeTokenAsync(
                new AuthorizationCodeTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId= "client-code",
                    ClientSecret="secret",
                    Code="10",
                    RedirectUri= "https://localhost:5051"
                }) ;

            if (tokenResponse.IsError)
            {
                string errorDesc = tokenResponse.ErrorDescription;
                if (string.IsNullOrEmpty(errorDesc)) errorDesc = "";
                if (errorDesc.Equals("invalid_username_or_password"))
                {
                    Console.WriteLine("用户名或密码错误，请重新输入！");
                }
                else
                {
                    Console.WriteLine($"[TokenResponse Error]: {tokenResponse.Error}, [TokenResponse Error Description]: {errorDesc}");
                }
            }
            else
            {

                Console.WriteLine($"Access Token: {tokenResponse.Json}");
                Console.WriteLine($"Access Token: {tokenResponse.RefreshToken}");
                Console.WriteLine($"Access Token: {tokenResponse.ExpiresIn}");
            }
            return tokenResponse.AccessToken;
        }

        /// <summary>
        /// 使用token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        private static async Task<string> UseAccessToken(string accessToken)
        {
            HttpClient client = new HttpClient();
            client.SetBearerToken(accessToken);
            HttpResponseMessage response = await client.GetAsync("https://localhost:5001/api/v1/Users/GetUsers");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API Request Error, StatusCode is : {response.StatusCode}");
                throw new FrameworkException($"API Request Error, StatusCode is : {response.StatusCode}");
            }
            else
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("");
                Console.WriteLine($"Result: {content}");

                // 3、输出结果到页面
                return content;
            }
        }
    }
}
