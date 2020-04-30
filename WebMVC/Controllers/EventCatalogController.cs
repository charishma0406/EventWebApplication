using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService _service;
        public EventCatalogController(IEventCatalogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var itemsOnpage = 10;

            var EventCatalog = await _service.GetEventCatalogItems(page?? 0, itemsOnpage);
            var vm = new CatalogIndexViewModel
            {
                EventDetails = EventCatalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnpage,
                    TotalItems = EventCatalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)EventCatalog.Count / itemsOnpage)
                }
            };

            return View(vm);
        }
    }
}