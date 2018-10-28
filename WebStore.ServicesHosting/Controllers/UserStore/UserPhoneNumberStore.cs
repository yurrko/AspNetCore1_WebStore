using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ServicesHosting.Controllers
{
    public partial class UsersApiController
    {
        #region IUserPhoneNumberStore

        [HttpPost("setPhoneNumber/{phoneNumber}")]
        public async Task SetPhoneNumberAsync([FromBody]User user, string phoneNumber)
        {
            await _userStore.SetPhoneNumberAsync(user, phoneNumber);
        }

        [HttpPost("getPhoneNumber")]
        public async Task<string> GetPhoneNumberAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberAsync(user);
        }

        [HttpPost("getPhoneNumberConfirmed")]
        public async Task<bool> GetPhoneNumberConfirmedAsync([FromBody]User user)
        {
            return await _userStore.GetPhoneNumberConfirmedAsync(user);
        }

        [HttpPost("setPhoneNumberConfirmed/{confirmed}")]
        public async Task SetPhoneNumberConfirmedAsync([FromBody]User user, bool confirmed)
        {
            await _userStore.SetPhoneNumberConfirmedAsync(user, confirmed);
        }


        #endregion
    }
}
