﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class EventDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Venue { get; set; }
        public decimal Price { get; set; }
        public string Age { get; set; }
        public int Occupancy { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string DateTime { get; set; }
        public string Day { get; set; }

        //creating prop for event type id for use of foriegn key
        public  int EventTypeId { get; set; }
        //navigational property
        public string  EventType { get; set; }
    }
}
