using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.services;

namespace WebMVC.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService _service;
        public EventCatalogController(IEventCatalogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int page)
        {
            var itemsOnpage = 10;

            var EventCatalog = await _service.GetEventCatalogItems(page, itemsOnpage);

            return View(EventCatalog);
        }
    }
}