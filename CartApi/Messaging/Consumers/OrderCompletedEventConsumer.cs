using CartApi.Models;
using Common.Messaging;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Messaging.Consumers
{
          //Iconsumer is as consumer of the ordercompletedevent from order we should know when the msg is received 
          //Iconsumer does for us that one
        public class OrderCompletedEventConsumer : IConsumer<OrderCompletedEvent>
        {
           //
            private readonly ICartRepository _repository;
            private readonly ILogger<OrderCompletedEventConsumer> _logger;
        //when we receive message we need to clean my redis cache that is y we are injecting cart here
            public OrderCompletedEventConsumer(ICartRepository repository, ILogger<OrderCompletedEventConsumer> logger)
            {

                _repository = repository;
                _logger = logger;
            }
        //this is the method this is the listner when ever we receive mesage this method will automatically called
            public async Task Consume(ConsumeContext<OrderCompletedEvent> context) 
            {
            //
                _logger.LogWarning("We are in consume method now...");
                _logger.LogWarning("BuyerId:" + context.Message.BuyerId);
                await _repository.DeleteCartAsync(context.Message.BuyerId);

            }
        }
}
