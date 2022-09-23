using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Streamye.Commons.Exceptions;

namespace Microsoft.Streamye.Commons.ModelBinds.Users
{
    public class SysUserModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            if (bindingContext.ModelType == typeof(SysUser))
            {
                SysUser sysUser = (SysUser)bindingContext.Model;
                HttpContext httpContext = bindingContext.HttpContext;
                ClaimsPrincipal claimsPrincipal = httpContext.User;
                IEnumerable<Claim> claims = claimsPrincipal.Claims;

                // 1、为空则表示没有登陆
                if (claims.ToList().Count == 0)
                {
                    throw new BizException("授权失败，没有登录");
                }

                foreach (var claim in claims)
                {
                    // 1、获取用户Id
                    if (claim.Type.Equals("sub"))
                    {
                        sysUser.UserId = Convert.ToInt32(claim.Value);
                    }

                    // 2、获取用户名
                    if (claim.Type.Equals("amr"))
                    {
                        sysUser.UserName = claim.Value;
                    }
                }

                // 3、返回结果
                bindingContext.Result = ModelBindingResult.Success(sysUser);
            }

            return Task.CompletedTask;
        }
    }
}