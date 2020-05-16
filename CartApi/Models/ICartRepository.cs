using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public interface ICartRepository
    {
        //we want to get a cart and give a buyer id if user is having cart give that otherwise craete new one
        Task<Cart> GetCartAsync(string cartId);

        //it will show all the users who are saving in the cart gives back ienumerable of data
        IEnumerable<string> GetUsers();

        //everytime if we are updating the cart this is useful for that
        Task<Cart> UpdateCartAsync(Cart basket);
        //if user deleyte the cart or convert into order we use this method
        Task<bool> DeleteCartAsync(string id);
    }
}
