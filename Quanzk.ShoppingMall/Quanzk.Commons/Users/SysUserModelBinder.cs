using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Quanzk.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Quanzk.Commons.Users
{
    /// <summary>
    /// 系统用户模型绑定
    /// 1、将HttpContext用户信息转换成为Sysuser
    /// 2、将Sysuser绑定到action方法参数
    /// </summary>
    public class SysUserModelBinder : IModelBinder
    {
        /// <summary>
        /// 模型绑定
        /// </summary>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (bindingContext.ModelType == typeof(SysUser))
            {
                var sysUser = new SysUser();
                // 设置模型值  这里可以自行调整支持 Cookie/Redis等
                HttpContext httpContext = bindingContext.HttpContext;
                ClaimsPrincipal claimsPrincipal = httpContext.User;

                IEnumerable<Claim> claims = claimsPrincipal.Claims;
                // 判断申明是否为空，如果为空，没有登录，抛出异常
                if (claims.ToList().Count == 0)
                {
                    throw new BusinessException("授权失败，没有登录");
                }
                foreach (var claim in claims)
                {
                    // 获取用户Id
                    if (claim.Type.Equals("sub"))
                    {
                        sysUser.UserId = Convert.ToInt32(claim.Value);
                    }
                    if (claim.Type.Equals("amr"))
                    {
                        sysUser.UserName = claim.Value;
                    }
                }
                // 返回结果
                bindingContext.Result = ModelBindingResult.Success(claims);
            }
            return Task.CompletedTask;
        }
    }
}
