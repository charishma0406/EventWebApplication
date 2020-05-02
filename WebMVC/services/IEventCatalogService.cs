using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.services
{
    public interface IEventCatalogService
    {

        Task<Event> GetEventCatalogItems(int page, int size,int? location, int? type);
        Task<IEnumerable<SelectListItem>> GetLocationAsync();
        Task<IEnumerable<SelectListItem>> GetTypesAsync();
    }
}
