using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.IdentityServer4.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 微服务API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("UserServices", "UserServices")
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            // 1、client 认证模式
            // 2、client用户密码认证模式
            // 3、授权码认证模式（code）
            // 4、简单认证模式
            return new List<Client> {
                // 客户端认证模式
                new Client{
                    ClientId="client",
                    // 没有交互性用户，使用clientid/secret实现认证
                    AllowedGrantTypes=GrantTypes.ClientCredentials,
                    // 用于认证的密码
                    ClientSecrets={new Secret("secret".Sha256())},
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes={ "UserServices" }
                },
                // 资源持有者认证模式
                new Client
                {
                    ClientId="client-password",
                    // 使用用户名密码交互式验证
                    AllowedGrantTypes=GrantTypes.ResourceOwnerPassword,
                    // 用于认证的密码
                    ClientSecrets={new Secret("secret".Sha256())},
                    // 客户端有权访问的范围（Scopes）
                    AllowedScopes={ "UserServices" }
                },
                new Client
                {
                    ClientId = "client-code",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    RedirectUris = { "https://localhost:5051/signin-oidc" },// 客户端地址
                    // 登录退出地址
                    PostLogoutRedirectUris = { "https://localhost:5051/signout-callback-oidc" },// 客户端回调地址
                    AllowedScopes = new List<string>{
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        // 启用服务授权支持
                        "UserServices"},
                    // 增加授权访问
                    AllowOfflineAccess = true
                }
            };
        }

        /// <summary>
        /// openid身份资源
        /// </summary>
        public static IEnumerable<IdentityResource> Ids => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

        /// <summary>
        /// 测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="alice",
                    Password="123456"
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="faith",
                    Password="123456",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "faith"),
                        new Claim(JwtClaimTypes.GivenName, "faith"),
                        new Claim(JwtClaimTypes.FamilyName, "shenzhen"),
                        new Claim(JwtClaimTypes.Email, "faith0308@163.com"),
                        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                        new Claim(JwtClaimTypes.WebSite, "http://www.quanzk.com")
                    }
                }
            };
        }
    }
}
