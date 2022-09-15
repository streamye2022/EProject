using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Streamye.Commons.Exceptions;
using Microsoft.Streamye.UserServices.Models;
using Microsoft.Streamye.UserServices.Services;

namespace Microsoft.Streamye.UserServices.Controllers
{
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await UserService.GetUsersAsync();
            return users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await UserService.GetUserByIdAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User User)
        {
            if (id != User.Id)
            {
                return BadRequest();
            }

            try
            {
                await UserService.UpdateAsync(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("更新成功");
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User User)
        {
            // 1、判断用户名是否重复
            if (await UserService.UserNameExistsAsync(User.UserName))
            {
                throw new BizException("用户名已经存在");
            }

            await UserService.CreateAsync(User);
            return CreatedAtAction("GetUser", new { id = User.Id }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var User = await UserService.GetUserByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            await UserService.DeleteAsync(User);
            return User;
        }

        private async Task<bool> UserExistsAsync(int id)
        {
            return await UserService.UserExistsAsync(id);
        }
    }
}