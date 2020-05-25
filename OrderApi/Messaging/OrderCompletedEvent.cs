using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Messaging
{
    public class OrderCompletedEvent
    {
        //buyer id is to be located in the message
        public string BuyerId { get; set; }

        //creating the constructor
        public OrderCompletedEvent(string buyerId)
        {
            BuyerId = buyerId;
        }
    }
}
