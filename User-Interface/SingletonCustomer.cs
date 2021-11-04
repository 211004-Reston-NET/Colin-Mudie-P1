
using Models;

namespace User_Interface
{
    public class SingletonCustomer
    {
        public static Customer customer = new Customer();
        public static Orders orders = new Orders();
        public static string location { get; set; }

            //when searching previous orders is it for store or customer?
        public static string orderType { get; set; }
            // when searching previous orders, the ID of either the customer or store to search.
        public static int storeOrCustID { get; set; }
    }
}




