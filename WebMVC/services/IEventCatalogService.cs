using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.services
{
    public interface IEventCatalogService
    {

        Task<Event> GetEventCatalogItems(int page, int size);
        
    }
}
