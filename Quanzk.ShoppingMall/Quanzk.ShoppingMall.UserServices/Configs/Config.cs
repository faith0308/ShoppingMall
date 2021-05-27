using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.Configs
{
    /// <summary>
    /// 
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 微服务API资源  Scopes
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("QuzkService", "QuzkService")
            };
        }

        /// <summary>
        /// 这个方法是来规范tooken生成的规则和方法的。一般不进行设置，直接采用默认的即可。
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// 客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client> {
            new Client
            {
                ClientId = "client",
                // 没有交互性用户，使用 clientid/secret 实现认证。
                // client认证模式
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // 用于认证的密码
                ClientSecrets = { new Secret("secret".Sha256()) },
                // 客户端有权访问的范围（Scopes）
                AllowedScopes = { "QuzkService" }
            },
            new Client
            {
                ClientId = "client-password",
                // 使用用户名密码交互式验证
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                // 用于认证的密码
                ClientSecrets = { new Secret("secret".Sha256()) },
                // 客户端有权访问的范围（Scopes）
                AllowedScopes = { "QuzkService" }
            },
            new Client
            {
                ClientId = "client-code",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,
                // 客户端地址
                RedirectUris = { "https://localhost:5012/signin-oidc" },
                // 登录退出地址
                PostLogoutRedirectUris = { "https://localhost:5012/signout-callback-oidc" },
                AllowedScopes = new List<string>{
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    // 启用服务授权支持
                    "QuzkService"},
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
                    Password="password"
                },
                new TestUser
                {
                    SubjectId="2",
                    Username="bob",
                    Password="password"
                }
            };
        }

    }
}
