using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    public partial class UsersApiController
    {
        #region IUserPasswordStore

        [HttpPost("setPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody]PasswordHashDto hashDto)
        {
            await _userStore.SetPasswordHashAsync(hashDto.User, hashDto.Hash);
            return hashDto.User.PasswordHash;
        }

        [HttpPost("getPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody]User user)
        {
            var result = await _userStore.GetPasswordHashAsync(user);
            return result;
        }

        [HttpPost("hasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody]User user)
        {
            return await _userStore.HasPasswordAsync(user);
        }


        #endregion
    }
}
