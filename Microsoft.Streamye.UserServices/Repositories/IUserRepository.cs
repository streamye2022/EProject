using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Streamye.UserServices.Models;

namespace Microsoft.Streamye.UserServices.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(string UserName);
        Task<User> GetUserByIdAsync(int id);
        Task CreateAsync(User User);
        Task UpdateAsync(User User);
        Task DeleteAsync(User User);
        Task<bool> UserExistsAsync(int id);
        Task<bool> UserNameExistsAsync(string UserName);
    }
}