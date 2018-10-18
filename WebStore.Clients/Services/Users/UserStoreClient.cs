using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebStore.Clients.Base;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : BaseClient, IUsersClient
    {
        public UsersClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/users";
        }

        protected sealed override string ServiceAddress { get; set; }

        #region IUserStore

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/userId";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/userName";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            var url = $"{ServiceAddress}/userName/{userName}";
            return PostAsync(url, user);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/normalUserName";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            var url = $"{ServiceAddress}/normalUserName/{normalizedName}";
            return PostAsync(url, user);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }


        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user";
            var result = await PutAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/delete";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/find/{userId}";
            return GetAsync<User>(url);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/user/normal/{normalizedUserName}";
            var result = await GetAsync<User>(url);
            return result;
        }

        #endregion
        
        public void Dispose()
        {
            Client.Dispose();
        }

    }
}
