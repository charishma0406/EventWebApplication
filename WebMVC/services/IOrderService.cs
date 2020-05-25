using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe;

namespace WebMVC.services
{
    public interface IOrderService
    {
        Task<List<Stripe.Order>> GetOrders();
        //Task<List<Order>> GetOrdersByUser(ApplicationUser user);
        Task<Stripe.Order> GetOrder(string orderId);
        Task<int> CreateOrder(Stripe.Order order);
        Task<int> CreateOrder(Models.OrderModels.Order order);
        //  Order MapUserInfoIntoOrder(ApplicationUser user, Order order);
        //  void OverrideUserInfoIntoOrder(Order original, Order destination);
    }
}
