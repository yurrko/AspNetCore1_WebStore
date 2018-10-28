using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    public partial class UsersApiController
    {
        #region IUserRoleStore

        [HttpGet("user/find/{userId}")]
        public async Task<User> FindByIdAsync(string userId)
        {
            var result = await _userStore.FindByIdAsync(userId);
            return result;
        }

        [HttpGet("user/normal/{normalizedUserName}")]
        public async Task<User> FindByNameAsync(string normalizedUserName)
        {
            var result = await _userStore.FindByNameAsync(normalizedUserName);
            return result;
        }

        [HttpPost("role/{roleName}")]
        public async Task AddToRoleAsync([FromBody]User user, string roleName)
        {
            await _userStore.AddToRoleAsync(user, roleName);
        }

        [HttpPost("role/delete/{roleName}")]
        public async Task RemoveFromRoleAsync([FromBody]User user, string roleName)
        {
            await _userStore.RemoveFromRoleAsync(user, roleName);
        }

        [HttpPost("roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody]User user)
        {
            return await _userStore.GetRolesAsync(user);
        }

        [HttpPost("inrole/{roleName}")]
        public async Task<bool> IsInRoleAsync([FromBody]User user, string roleName)
        {
            return await _userStore.IsInRoleAsync(user, roleName);
        }

        [HttpGet("usersInRole/{roleName}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            return await _userStore.GetUsersInRoleAsync(roleName);
        }



        #endregion
    }
}
