using System;

namespace Microsoft.Streamye.UserServices.Models
{
    public class User
    {
        public int Id { set; get; } // 主键
        public string UserName { set; get; } // 用户名
        public string Password { set; get; } // 密码
        public string UserNickname { set; get; } // 用户昵称
        public string UserQQ { set; get; } // 用户QQ
        public DateTime CreateTime { set; get; } // 创建时间
        public string UserPhone { set; get; } // 用户手机号
    }
}