using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index(int? page, int? locationFilteredApplied, int? typesFilteredApplied)
        {
            var itemsOnpage = 10;

            var EventCatalog = await _service.GetEventCatalogItems(page?? 0, itemsOnpage, locationFilteredApplied, typesFilteredApplied);
            var vm = new CatalogIndexViewModel
            {
                EventDetails = EventCatalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnpage,
                    TotalItems = EventCatalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)EventCatalog.Count / itemsOnpage)
                },

                Locations = await _service.GetLocationAsync(),
                Types = await _service.GetTypesAsync(),

                LocationFilterApplied = locationFilteredApplied ?? 0,
                TypesFilterApplied = typesFilteredApplied ?? 0
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            //we are sending them to view
            return View();
        }
    }
}