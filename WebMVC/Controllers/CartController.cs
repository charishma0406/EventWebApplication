using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartApi.Models;
using EventApp1.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using WebMVC.Models.CartModels;
using WebMVC.services;

namespace WebMVC.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
       

            private readonly ICartService _cartService;
            private readonly IEventCatalogService _catalogService;
            private readonly IIdentityService<ApplicationUser> _identityService;

            public CartController(IIdentityService<ApplicationUser> identityService, ICartService cartService, IEventCatalogService catalogService)
            {
                _identityService = identityService;
                _cartService = cartService;
                _catalogService = catalogService;
            }
            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Index(Dictionary<string, int> quantities, string action)
            {
                if (action == "[ Checkout ]")
                {
                    return RedirectToAction("Create", "Order");
                }


                try
                {
                    var user = _identityService.Get(HttpContext.User);
                    var basket = await _cartService.SetQuantities(user, quantities);
                    var vm = await _cartService.UpdateCart(basket);

                }
                catch (BrokenCircuitException)
                {
                    // Catch error when CartApi is in open circuit  mode                 
                    HandleBrokenCircuitException();
                }

                return View();

            }
            // It is going to get the product details that yo uwant to add
            public async Task<IActionResult> AddToCart(EventDetails productDetails)
            {
                try
                {
                //are you really giving me a product details
                    if (productDetails.Id > 0)
                    {
                    //asking identity service to give current user
                        var user = _identityService.Get(HttpContext.User);
                    //
                        var product = new CartItem()
                        {
                            //it will give the id
                            Id = Guid.NewGuid().ToString(),
                            Quantity = 1,
                            ProductName = productDetails.Name,
                            PictureUrl = productDetails.PictureUrl,
                            UnitPrice = productDetails.Price,
                            ProductId = productDetails.Id.ToString()
                        };
                    //here we are firing cart service and adding item to the cart
                        await _cartService.AddItemToCart(user, product);
                    }
                    //once it added it reirects back to the home page
                    return RedirectToAction("Index", "Catalog");
                }
            //this broken circuit execption is to check whether our service is broken
                catch (BrokenCircuitException)
                {
                    // Catch error when CartApi is in circuit-opened mode                 
                    HandleBrokenCircuitException();
                }

                return RedirectToAction("Index", "Catalog");

            }

            //here it says cart service is down cannot add to the cart please try later on
            private void HandleBrokenCircuitException()
            {
                TempData["BasketInoperativeMsg"] = "cart Service is inoperative, please try later on. (Business Msg Due to Circuit-Breaker)";
            }

        }
    }