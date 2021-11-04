using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface IOrderBL
    {
        /// <summary>
        ///     This method will return a list of Orders that belong to either a customer or a Storefront
        /// </summary>
        /// <param name="p_customer_or_store"> can be either "store" or "customer"</param>
        /// <param name="p_id"> the matching ID corresponding to either the store or customer to find the list of orders from.</param>
        /// <returns> returns a list of Models.Orders </returns>
        List<Orders> GetOrdersList(string p_customer_or_store, int p_id);
    }
}