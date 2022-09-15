using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.UserServices.Context;
using Microsoft.Streamye.UserServices.Models;

namespace Microsoft.Streamye.UserServices.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private UserContext UserContext;

        public UserRepository(UserContext userContext)
        {
            this.UserContext = userContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await UserContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(string UserName)
        {
            return await UserContext.Users.FirstAsync(user => user.UserName == UserName);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await UserContext.Users.FindAsync(id);
        }

        public async Task CreateAsync(User User)
        {
            await UserContext.Users.AddAsync(User);
            await UserContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User User)
        {
            UserContext.Users.Update(User);
            await UserContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User User)
        {
            UserContext.Users.Remove(User);
            await UserContext.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await UserContext.Users.AnyAsync(User => User.Id == id);
        }

        public async Task<bool> UserNameExistsAsync(string UserName)
        {
            return await UserContext.Users.AnyAsync(User => User.UserName == UserName);
        }
    }
}