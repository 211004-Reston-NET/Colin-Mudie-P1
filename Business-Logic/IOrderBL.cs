using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface IOrderBL
    {
        /// <summary>
        /// This method will return a list of Orders that belong to a Storefront
        /// </summary>
        /// <param name="p_storeId"> the matching ID corresponding to either the store to find the list of orders from.</param>
        /// <returns> returns a list of Orders </returns>
        List<Order> GetOrdersListForStore(int p_storeId);

        /// <summary>
        /// This method will return a list of Orders that belong to a Customer
        /// </summary>
        /// <param name="p_customerId"> the matching ID corresponding to either the customer to find the list of orders from.</param>
        /// <returns> returns a list of Orders </returns>
        List<Order> GetOrdersListForCustomer(string p_customerId);
    }
}