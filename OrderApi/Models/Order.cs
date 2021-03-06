﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models
{
    public class Order
    {
        //marking orderd id as a primary key in the database
        [Key]
        //it is like autogenerated column it is an identity column
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //order id
        public int OrderId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderDate { get; set; }
        public string BuyerId { get; set; }

        public string UserName { get; set; }

        //order status it is enum
        public OrderStatus OrderStatus { get; set; }

        //address where it needs toi go
        public string Address { get; set; }
        //
        public string PaymentAuthCode { get; set; }
        // public Guid RequestId { get;  set; }
        //orderd total
        public decimal OrderTotal { get; set; }
        //list of orderitems
        public IEnumerable<OrderItem> OrderItems { get; set; }
        //protected like children can see this
        protected Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
    //this is my orderd status
    public enum OrderStatus
    {
        //is the order preparing
        Preparing = 1,
        //order shipped
        Shipped = 2,
        //is our order deliverd
        Delivered = 3,
    }
}
