using System;
using System.Collections.Generic;

using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Messaging;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OrderApi.Data;
using OrderApi.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrdersContext _ordersContext;

        private readonly IConfiguration _config;

        private readonly ILogger<OrderController> _logger;
        //for  publish the message
        private IPublishEndpoint _bus;
        public OrderController(OrdersContext ordersContext,
            ILogger<OrderController> logger,
            IConfiguration config
            //we want to publish endpoint for my bus
          , IPublishEndpoint bus
            )
        {
            _config = config;

            _ordersContext = ordersContext ?? throw new ArgumentNullException(nameof(ordersContext));

            ordersContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            //if one service wants to send the messages to another service for messages for  that we use service bus
            _bus = bus;
            _logger = logger;
        }

        // POST api/Order/new
        [Route("new")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        //this api will get called when the chekout button
        //we are accepting the actual order
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            //after we orderd the products we need to show it is preparing so that is y we are using this line
            order.OrderStatus = OrderStatus.Preparing;
            //current date and time 
            order.OrderDate = DateTime.UtcNow;

            _logger.LogInformation(" testing ");
            //logging in with the logging credentials
            _logger.LogInformation(" In Create Order");
            _logger.LogInformation(" Order" + order.UserName);

            //ading order to the order table in the data base
            _ordersContext.Orders.Add(order);
            //if we want to add multiple items we use add range inbuilt method to the items
            _ordersContext.OrderItems.AddRange(order.OrderItems);

            //we have to save the order after adding into the data base  
            _logger.LogInformation(" Order added to context");
            _logger.LogInformation(" Saving........");
            //for saving in the data base we are using try catch block
            try
            {
                //calling my entityframwork data base, if everything goes well the data should be in my data base
                await _ordersContext.SaveChangesAsync();
                _logger.LogWarning("BuyerId is: " + order.BuyerId);
                //publishing my message pass the buyer id
                _bus.Publish(new OrderCompletedEvent(order.BuyerId)).Wait();
                //here is the new order id, this what we are giving back to the caller of the api
                return Ok(new { order.OrderId });
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                // if you find anything wrong then give this error
                _logger.LogError("An error occured during Order saving .." + ex.Message);
                //sending the bad request
                return BadRequest();
            }

        }

        [HttpGet("{id}", Name = "GetOrder")]
        //[Route("{id}")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        //this one if we need to see the existing order
        public async Task<IActionResult> GetOrder(int id)
        {
            //going to the orders table,
            var item = await _ordersContext.Orders
                //include mean swith the order id give me all the orders that the order contains
                .Include(x => x.OrderItems)
                .SingleOrDefaultAsync(ci => ci.OrderId == id);
            //if the item is null
            if (item != null)
            {
                return Ok(item);
            }
            //if it is not there then return not found
            return NotFound();

        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //this is used for we wan to see all the orders
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _ordersContext.Orders.ToListAsync();


            return Ok(orders);
        }


    }
}