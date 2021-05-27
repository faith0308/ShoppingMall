using IdentityModel;
using IdentityServer4.Validation;
using Quanzk.Commons.Exceptions;
using Quanzk.ShoppingMall.UserServices.Models;
using Quanzk.ShoppingMall.UserServices.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Quanzk.ShoppingMall.UserServices.IdentityServer
{
    /// <summary>
    /// 自定义资源持有者验证(从数据库获取用户信息进行验证)
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        /// <summary>
        /// 用户服务
        /// </summary>
        public readonly IUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService"></param>
        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 用户登录授权验证 Id4 jwt
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // 根据用户名获取用户对象
            var user = _userService.GetUserByName(context.UserName);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    if (!user.Password.Equals(context.Password))
                    {
                        throw new BusinessException($"用户密码不正确");
                    }
                    context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: user.UserName,
                        claims: GetUserClaims(user));
                }
                else
                {
                    throw new BusinessException($"用户密码信息丢失");
                }
            }
            else
            {
                throw new BusinessException($"用户不存在");
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// 用户身份声明
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Claim[] GetUserClaims(User user)
        {
            return new Claim[] {
                new Claim(JwtClaimTypes.Subject,user.Id.ToString()??""),
                new Claim(JwtClaimTypes.Id, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.UserName?? ""),
                new Claim(JwtClaimTypes.PhoneNumber, user.UserPhone  ?? "")
            };
        }
    }
}
