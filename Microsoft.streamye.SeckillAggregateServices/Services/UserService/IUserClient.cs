using Microsoft.Streamye.Cores.MicroClients.Attributes;
using Microsoft.Streamye.SeckillAggregateServices.Models.UserModel;

namespace Microsoft.Streamye.SeckillAggregateServices.Services.UserService
{
    [MicroClient("http", "UserServices")]
    public interface IUserClient
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [PostPath("/Users")]
        public User RegistryUsers(User user);
    }
}