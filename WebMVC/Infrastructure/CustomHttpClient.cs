using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class CustomHttpClient : IHttpClient
    {
        //http client is component/protocol/class in the .netcore.
        //why we are not injecting because we need http client everywhere that is y we are not using dependcy inje here
        private readonly HttpClient _client;
        //creating customer and instatiating new http client here
        public CustomHttpClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetStringAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
      
            var requestmessage = new HttpRequestMessage(HttpMethod.Get, uri);
      
            if (authorizationToken != null)
            {

            }

            var response = await _client.SendAsync(requestmessage);
        
            return await response.Content.ReadAsStringAsync();
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T items, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T items, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
