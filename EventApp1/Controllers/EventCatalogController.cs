using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp1.Data;
using EventApp1.Domain;
using EventApp1.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCatalogController : ControllerBase
    {
        //to need access for the event context we are adding this
        private readonly EventContext _context;
        //to need access for the configuration we are adding this, startup already know to get the iconfiguration for to to access the appjson config file
        private readonly IConfiguration _config;

        //creating constructor and injecting the eventcontext data and for changing the url in the event seed we are configuring the base url in the app.json file and configuring that here 
        public EventCatalogController(EventContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        [Route("[action]")]
        //whenever we see return type for async and await we need to put task
        public async Task<IActionResult> Details([FromQuery] int pagesize = 0, [FromQuery]int pageindex = 6)
        {
            //creating var for the count of the pages and longcountasync means it will count till long
            var itemcount = await _context.Event_Details.LongCountAsync();


            //getting all the catalog items  and skiping and taking how many records we need
            var items = await _context.Event_Details.Skip(pageindex * pagesize).Take(pagesize).ToListAsync();


            items = ChangePictureUrl(items);


            //creating object for the paginateddetailsviewmodel class
            var model = new PaginatedDetailsViewModel<EventDetails>
            {
                PageIndex = pageindex,
                PageSize = pagesize,
                Count = itemcount,
                Data = items
            };
            //controller is connected to view model and returning the model back in that
            //model pg siz, pg ind,count of pg and data will be available
            return Ok(model);


        }
        //for changing the pictuire url we are creating this method
        private List<EventDetails> ChangePictureUrl(List<EventDetails> items)
        {
            //replacing the picture url with the local http url and why we are saving that in
            //picture url variable because string is immutable, that means string will not change if we replace also so we are saving in picture url variable

            items.ForEach(d => d.PictureUrl = d.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced",_config["ExternalBaseUrl"]));
            return (items);
        }

        


    }
}