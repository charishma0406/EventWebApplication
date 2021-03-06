﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Infrastructure
{
    public class ApiPaths
    {

        //we are writing class in the class because for here we are creating all the end points for multiple ones in the future.
        public static class EventCatalog
        {
            //service will ask api path can you tell me the path(the end point) that get all the catalog items.
            //goal is to return all the catalog items in one url that is y we are creating a method here

            //we are giving the base uri because it should know the which local host and which port it is using and that port it will take
            //we are giving the page size(page) , page index(take)how many pages to take, which type , which brand to take.
            public static string GetAllEventCatalogItems(string baseUri, int page, int take, int?location, int?type)
            {
                var filterQs = string.Empty;
                if(location.HasValue || type.HasValue)
                {
                    var locationQs = (location.HasValue) ? location.Value.ToString() : "null";

                    var typesQS = (type.HasValue) ? type.Value.ToString() : "null";
                    filterQs = $"/location/{locationQs}/type/{typesQS}";
                }




                return $"{baseUri}/Details{filterQs}?pageIndex={page}&pageSize={take}";
            }



            public static string GettAllLocations(string baseUri)
            {
                return $"{baseUri}/eventlocations";
            }

            public static string GetAllTypes(string baseUri)
            {
                return $"{baseUri}/eventtypes";
            }
        }


        public static class Basket
        {
            //all these methods should match cart in the services
            //get basket
            public static string GetBasket(string baseUri, string basketId)
            {
                //the baseuri and the id of the basket to get the basket
                //baseuri is api/cart/get ation in the cart controller services side
                return $"{baseUri}/{basketId}";
            }

            //update basket
            public static string UpdateBasket(string baseUri)
            {
                //
                return baseUri;
            }

            //delete basket
            public static string CleanBasket(string baseUri, string basketId)
            {
                return $"{baseUri}/{basketId}";
            }
        }

        public static class Order
        {
            public static string GetOrder(string baseUri, string orderId)
            {
                return $"{baseUri}/{orderId}";
            }

            //public static string GetOrdersByUser(string baseUri, string userName)
            //{
            //    return $"{baseUri}/userOrders?userName={userName}";
            //}
            public static string GetOrders(string baseUri)
            {
                return baseUri;
            }
            public static string AddNewOrder(string baseUri)
            {
                return $"{baseUri}/new";
            }
        }

    }
}
