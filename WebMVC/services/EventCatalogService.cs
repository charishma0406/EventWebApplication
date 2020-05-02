using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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


        public async Task<Event> GetEventCatalogItems(int page, int size, int? location, int? type)
        {
          var EventCatalogItemsUri =   ApiPaths.EventCatalog.GetAllEventCatalogItems(_baseUri,page,size, location, type);

            var datastring = await _client.GetStringAsync(EventCatalogItemsUri);
            return JsonConvert.DeserializeObject<Event>(datastring);
        }

        public async Task<IEnumerable<SelectListItem>> GetLocationAsync()
        {
            var locationUri = ApiPaths.EventCatalog.GettAllLocations(_baseUri);
            var dataString = await _client.GetStringAsync(locationUri);
            var items = new List<SelectListItem>
            { 
                new SelectListItem
                { 
                    Value = null,
                    Text = "All",
                    Selected = true
                }

            };

            var locations = JArray.Parse(dataString);
            foreach(var location in locations)
            {
                items.Add(new SelectListItem
                {
                    Value = location.Value<string>("id"),
                    Text = location.Value<string>("location"),
                }
                ); 

            }
            return items;

        }

        public async Task<IEnumerable<SelectListItem>> GetTypesAsync()
        {
            var typeUri = ApiPaths.EventCatalog.GetAllTypes(_baseUri);
            var dataString = await _client.GetStringAsync(typeUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }

            };

            var types = JArray.Parse(dataString);

            foreach(var type in types)
            {
                items.Add(new SelectListItem
                {
                    Value = type.Value<string>("id"),
                    Text = type.Value<string>("type"),
                }
                );
            }
            return items;
        }
    }
}
