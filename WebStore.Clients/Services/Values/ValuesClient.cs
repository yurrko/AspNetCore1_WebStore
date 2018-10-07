﻿using System;
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
        public ValuesClient( IConfiguration configuration ) : base( configuration )
        {
            ServiceAddress = "api/values";
        }

        protected sealed override string ServiceAddress { get; set; }

        public IEnumerable<string> Get()
        {
            var list = new List<string>();
            var response = Client.GetAsync( $"{ServiceAddress}" ).Result;
            if ( response.IsSuccessStatusCode )
            {
                list = response.Content.ReadAsAsync<List<string>>().Result;
            }
            return list;
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            var list = new List<string>();
            var response = await Client.GetAsync( $"{ServiceAddress}" );
            if ( response.IsSuccessStatusCode )
            {
                list = await response.Content.ReadAsAsync<List<string>>();
            }
            return list;
        }

        public string Get( int id )
        {
            var result = string.Empty;

            var response = Client.GetAsync( $"{ServiceAddress}/get/{id}" ).Result;
            if ( response.IsSuccessStatusCode )
            {
                result = response.Content.ReadAsAsync<string>().Result;
            }
            return result;
        }

        public async Task<string> GetAsync( int id )
        {
            var result = string.Empty;

            var response = await Client.GetAsync( $"{ServiceAddress}/get/{id}" );
            if ( response.IsSuccessStatusCode )
            {
                result = await response.Content.ReadAsAsync<string>();
            }
            return result;
        }

        public Uri Post( string value )
        {
            var response = Client.PostAsJsonAsync( $"{ServiceAddress}", value ).Result;
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public async Task<Uri> PostAsync( string value )
        {
            var response = await Client.PostAsJsonAsync( $"{ServiceAddress}", value );
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        public HttpStatusCode Put( int id, string value )
        {
            var response = Client.PutAsJsonAsync( $"{ServiceAddress}/{id}", value ).Result;
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutAsync( int id, string value )
        {
            var response = await Client.PutAsJsonAsync( $"{ServiceAddress}/{id}", value );
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public HttpStatusCode Delete( int id )
        {
            var response = Client.DeleteAsync( $"{ServiceAddress}/{id}" ).Result;
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteAsync( int id )
        {
            var response = await Client.DeleteAsync( $"{ServiceAddress}/{id}" );
            return response.StatusCode;
        }
    }
}
