namespace Microsoft.Streamye.SeckillAggregateServices.Dtos.UserDto
{
    public class UserDto
    {
        public string AccessToken { set; get; } // 执行token(用户身份)
        public int ExpiresIn { set; get; } // AccessToken过期时间
        public string UserName { set; get; } // 用户名
    }
}