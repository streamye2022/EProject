using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.UserServices.Context;
using Microsoft.Streamye.UserServices.Models;
using Microsoft.Streamye.UserServices.Repositories;

namespace Microsoft.Streamye.UserServices.Services.Impl
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task<User> GetUserAsync(string UserName)
        {
            return await userRepository.GetUserAsync(UserName);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await userRepository.GetUserByIdAsync(id);
        }

        public async Task CreateAsync(User User)
        {
            await userRepository.CreateAsync(User);
        }

        public async Task UpdateAsync(User User)
        {
            await userRepository.UpdateAsync(User);
        }

        public async Task DeleteAsync(User User)
        {
            await userRepository.DeleteAsync(User);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await userRepository.UserExistsAsync(id);
        }

        public async Task<bool> UserNameExistsAsync(string UserName)
        {
            return await userRepository.UserNameExistsAsync(UserName);
        }
    }
}