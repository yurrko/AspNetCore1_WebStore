using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient
    {
        #region IUserLoginStore

        public Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/addLogin";
            return PostAsync(url, new AddLoginDto() { User = user, UserLoginInfo = login });
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/removeLogin/{loginProvider}/{providerKey}";
            return PostAsync(url, user);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/getLogins";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<UserLoginInfo>>();
        }

        public Task<User> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/findbylogin/{loginProvider}/{providerKey}";
            return GetAsync<User>(url);
        }

        #endregion
    }
}
