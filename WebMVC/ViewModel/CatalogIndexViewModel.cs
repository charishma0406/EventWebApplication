﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebMVC.Models;

namespace WebMvc.ViewModels
{
    public class CatalogIndexViewModel
    {
        public PaginationInfo PaginationInfo { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<EventDetails> EventDetails { get; set; }

        public int? TypesFilterApplied { get; set; }

    }
}
