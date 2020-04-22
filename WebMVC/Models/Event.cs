using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class Event
    {

        //how much page size we are having
        public int PageSize { get; set; }

        // which page we are right now
        public int PageIndex { get; set; }

        //the total number of data or records
        public long Count { get; set; }

        //using the generics, we can use any data in that tentity here we are using eventdetails while calling the class
        public List<EventDetails> Data { get; set; }
    }
}
