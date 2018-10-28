using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient
    {
        #region IUserRoleStore

        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/role/{roleName}";
            return PostAsync(url, user);
        }

        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/delete/{roleName}";
            return PostAsync(url, user);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/roles";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<IList<string>>();
        }

        public async Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/inrole/{roleName}";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/usersInRole/{roleName}";
            IList<User> result = await GetAsync<List<User>>(url);
            return result.ToList();
        }

        #endregion
    }
}
