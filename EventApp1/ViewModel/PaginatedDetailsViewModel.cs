using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp1.ViewModel
{

    //creating view model for the ui binding 
    //making this class as generics because we can use this anywhere instead of hard coding the data 
    //like catalog items, carts and orders for evrything we should use all these
    //class is for reference type because our data is reference type we are using class 
   
    public class PaginatedDetailsViewModel<TEntity> where TEntity : class
    {
        //how much page size we are having
        public int PageSize { get; set; }

        // which page we are right now
        public int PageIndex { get; set; }

        //the total number of data or records
        public long Count { get; set; }

        //using the generics, we can use any data in that tentity here we are using eventdetails while calling the class
        public IEnumerable<TEntity> Data { get; set; }
    }
}
