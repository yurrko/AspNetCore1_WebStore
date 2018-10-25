using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces.Api;

namespace WebStore.Clients.Services.Values
{
    public class ValuesClient : BaseClient, IValuesService
    {
        public ValuesClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/values";
        }

        protected sealed override string ServiceAddress { get; set; }

        public IEnumerable<string> Get()
        {
            var url = $"{ServiceAddress}";
            var result = Get<List<string>>(url);
            return result;
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            var url = $"{ServiceAddress}";
            var result = await GetAsync<List<string>>(url);
            return result;
        }

        public string Get(int id)
        {
            var result = string.Empty;

            var response = Client.GetAsync($"{ServiceAddress}/get/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<string>().Result;
            }
            return result;
        }

        public async Task<string> GetAsync(int id)
        {
            var result = string.Empty;

            var response = await Client.GetAsync($"{ServiceAddress}/get/{id}");
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }

        public Uri Post(string value)
        {
            var url = $"{ServiceAddress}/post";
            var response = Post(url, value);
            return response.Headers.Location;
        }

        public async Task<Uri> PostAsync(string value)
        {
            var url = $"{ServiceAddress}/post";
            var response = await PostAsync(url, value);
            return response.Headers.Location;
        }

        public HttpStatusCode Put(int id, string value)
        {
            var url = $"{ServiceAddress}/put/{id}";
            var response = Put(url, value);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutAsync(int id, string value)
        {
            var url = $"{ServiceAddress}/put/{id}";
            var response = await PutAsync(url, value);
            return response.StatusCode;
        }

        public HttpStatusCode Delete(int id)
        {
            var url = $"{ServiceAddress}/delete/{id}";
            var response = Delete(url);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteAsync(int id)
        {
            var url = $"{ServiceAddress}/delete/{id}";
            var response = await DeleteAsync(url);
            return response.StatusCode;
        }
    }
}
