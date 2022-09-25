using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.Streamye.Commons.Exceptions;
using Microsoft.Streamye.UserServices.Models;
using Microsoft.Streamye.UserServices.Services;

namespace Microsoft.Streamye.UserServices.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public readonly IUserService userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // 1、根据用户名获取用户
            User user = await userService.GetUserAsync(context.UserName);

            // 2、判断User
            if (user == null)
            {
                throw new BizException($"数据库用户不存在:{context.UserName}");
            }

            if (!context.Password.Equals(user.Password))
            {
                throw new BizException($"数据库用户密码不正确");
            }
            //  var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(GetUserClaims(user)));

            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: user.UserName,
                claims: GetUserClaims(user));
            await Task.CompletedTask;
        }

        public Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Id, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.UserName ?? ""),
                new Claim(JwtClaimTypes.PhoneNumber, user.UserPhone ?? "")
            };
        }
    }
}