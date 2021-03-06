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

        /// <summary>
        ///     Will Find the Customer to add order to by CustomerId saved in Order, then add the Order them and send to DB.
        /// </summary>
        /// <param name="p_order"> The Order that will be added to the list of Orders on our p_customer </param>
        void PlaceOrder(Order p_order);

        /// <summary>
        /// Finds and returns an order with the matching ID.
        /// </summary>
        /// <param name="p_orderId"> The ID to search for </param>
        /// <returns>The Order found</returns>
        Order GetOrderById(int p_orderId);
    }
}