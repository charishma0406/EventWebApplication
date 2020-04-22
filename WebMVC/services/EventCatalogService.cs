using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using WebMVC.Infrastructure;
using WebMVC.Models;

namespace WebMVC.services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly string _baseUri;
        private readonly IHttpClient _client;

        public EventCatalogService(IConfiguration config, IHttpClient client)
        {
            _baseUri = $"{config["EventCatalogUrl"]}/api/EventCatalog";
            _client = client;
        }


        public async Task<Event> GetEventCatalogItems(int page, int size)
        {
          var EventCatalogItemsUri =   ApiPaths.EventCatalog.GetAllEventCatalogItems(_baseUri,page,size);

            var datastring = await _client.GetStringAsync(EventCatalogItemsUri);
            return JsonConvert.DeserializeObject<Event>(datastring);
        }
    }
}
