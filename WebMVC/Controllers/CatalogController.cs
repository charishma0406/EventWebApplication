using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IEventCatalogService _service;
        public CatalogController(IEventCatalogService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index(int? page, int? locationFilteredApplied,
            int? typesFilterApplied)
        {
            var itemsOnPage = 10;

            var catalog = await _service.GetEventCatalogItems(page ?? 0, itemsOnPage,
                locationFilteredApplied, typesFilterApplied);
            var vm = new CatalogIndexViewModel
            {
                EventDetails = catalog.Data,
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = page ?? 0,
                    ItemsPerPage = itemsOnPage,
                    TotalItems = catalog.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsOnPage)
                },
                Locations = await _service.GetLocationAsync(),
                Types = await _service.GetTypesAsync(),
                LocationFilterApplied = locationFilteredApplied ?? 0,
                TypesFilterApplied = typesFilterApplied ?? 0
            };


            return View(vm);

        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";


            return View();
        }
    }
}