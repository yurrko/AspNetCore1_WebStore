using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    public partial class UsersApiController
    {
        #region IUserTwoFactorStore

        [HttpPost("setTwoFactor/{enabled}")]
        public async Task SetTwoFactorEnabledAsync([FromBody]User user, bool enabled)
        {
            await _userStore.SetTwoFactorEnabledAsync(user, enabled);
        }

        [HttpPost("getTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody]User user)
        {
            return await _userStore.GetTwoFactorEnabledAsync(user);
        }


        #endregion
    }
}
