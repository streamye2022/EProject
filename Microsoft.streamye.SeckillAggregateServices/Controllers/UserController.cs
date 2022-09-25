using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Streamye.Commons.AutoMappers;
using Microsoft.Streamye.SeckillAggregateServices.Dtos.UserDto;
using Microsoft.Streamye.SeckillAggregateServices.Models.UserModel;
using Microsoft.Streamye.SeckillAggregateServices.Services.UserService;

namespace Microsoft.Streamye.SeckillAggregateServices.Controllers
{
    [Route("api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserClient userClient;

        public UserController(IUserClient userClient)
        {
            this.userClient = userClient;
        }

        [HttpPost("Register")]
        public User Post([FromForm] UserRegistryDto userForm)
        {
            // 转换
            User user = userForm.AutoMapTo<UserRegistryDto, User>();
            user.CreateTime = new DateTime();

            // 用户进行注册
            user = userClient.RegistryUsers(user);

            return user;
        }

        // [HttpPost("Login")]
        //  public UserDto PostLogin([FromForm] UserLoginDto loginForm)
        // {
        //     // 1、查询用户信息 
        //     // 2、判断用户信息是否存在
        //     // 3、将用户信息生成token进行存储
        //     // 4、将token信息存储到cookie或者session中
        //     // 5、返回成功信息和token
        //     // 6、对于token进行认证(也就是身份认证)
        //
        //     // 1、获取IdentityServer接口文档
        //     HttpClient client = new HttpClient();
        //     // DiscoveryDocumentResponse discoveryDocument = client.GetDiscoveryDocumentAsync("http://localhost:5005").Result;
        //     //"https://userservices:80"
        //     var discoveryDocument = client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        //     {
        //         Address = "http://userservices:80",
        //         Policy =
        //         {
        //             RequireHttps = false
        //         }
        //     }).Result;
        //     if (discoveryDocument.IsError)
        //     {
        //         throw new BizException(discoveryDocument.Error);
        //     }
        //
        //     // 2、根据用户名和密码建立token
        //     TokenResponse tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest()
        //     {
        //         Address = discoveryDocument.TokenEndpoint,
        //         ClientId = "client-password",
        //         ClientSecret = "secret",
        //         GrantType = "password",
        //         UserName = loginForm.UserName,
        //         Password = loginForm.Password
        //     }).Result;
        //     // 3、返回AccessToken
        //     if (tokenResponse.IsError)
        //     {
        //         throw new BizException(tokenResponse.Error + "," + tokenResponse.Raw);
        //     }
        //
        //     // 4、返回UserDto信息
        //     UserDto userDto = new UserDto();
        //     userDto.UserName = loginForm.UserName;
        //     userDto.AccessToken = tokenResponse.AccessToken;
        //     userDto.ExpiresIn = tokenResponse.ExpiresIn;
        //
        //     return userDto;
        // }
    }
}