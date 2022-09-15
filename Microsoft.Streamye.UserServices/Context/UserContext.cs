using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.UserServices.Models;

namespace Microsoft.Streamye.UserServices.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { set; get; }
    }
}