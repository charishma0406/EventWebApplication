using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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


        private async  Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken, string authorizationMethod)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call
            //
            var requestMessage = new HttpRequestMessage(method, uri);
            //serialising the object into json string
            Console.WriteLine(JsonConvert.SerializeObject(item));
            //content of a req message now it has a body
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");
            //if my authorization token is not null
            if (authorizationToken != null)
            {
                //then fire the call
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500 
            // needed for circuit breaker to track fails

            //if there are internal server error or like that
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T items, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, items, authorizationToken, authorizationMethod);
        }


        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T items, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, items, authorizationToken, authorizationMethod);
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            throw new NotImplementedException();
        }
    }
}
