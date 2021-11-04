using System.Collections.Generic;
using Models;
namespace Data_Access_Logic
{
    public interface IRepository
    {
        /// <summary>
        /// adds a customer to our database.
        /// </summary>
        /// <param name="p_customer"> this is the Customer class that will be added to the db </param>
        /// <returns> will return the </returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// This will return a list of Customers stored in the database
        /// </summary>
        /// <returns> will return a list of Customers</returns>
        List<Customer> GetCustomerList();

        /// <summary>
        ///     This will return a list of Store Fronts from the db.
        /// </summary>
        /// <returns> will return a list of StoreFronts.</returns>
        List<StoreFront> GetStoreFrontList();

        /// <summary>
        ///     This will return a list of products from the db.
        /// </summary>
        /// <param name="p_storeId"> The ID of the store to retrieve the Line Items from. </param>
        /// <returns> will return a list of products. </returns>
        List<LineItems> GetLineItemsList(int p_storeId);

        /// <summary>
        ///     This will grab the current list of customers, then grab the current list of Orders for our selected Customer,
        ///     Then it will add our new order to the list and send back to the db.
        /// </summary>
        /// <param name="p_customer"> The customer that will be edited from the List of Customers </param>
        /// <param name="p_order"> The Order that will be added to the list of Orders on our p_customer </param>
        /// <returns> Will return the Order that was placed. </returns>
        Orders PlaceOrder(Customer p_customer, Orders p_order);

        /// <summary>
        /// This will grab the current list of products from db.
        /// </summary>
        /// <returns> Will return a list of products </returns>
        List<Products> GetAllProducts();

        /// <summary>
        /// This will return 1 product and will require the products product_id.
        /// </summary>
        /// <param name="p_productId"> the product_id that will match in the database </param>
        /// <returns> returns a single Products class </returns>
        Products GetProductByProductId(int p_productId);

        /// <summary>
        /// This will get all orders from DB, then will find the most recent order and return it.
        /// It is used for PlaceOrder(), so that we can find the order_id of the order that was just placed.
        /// </summary>
        /// <returns> Last order_id that was just placed. </returns>
        int GetLastOrderId();

        /// <summary>
        /// Will update the stock of the 
        /// </summary>
        /// <param name="p_orderId"> the order id that will be added to the list of LineItems in p_order. </param>
        /// <param name="p_order"> Order that contains the line Items to add order_id to </param>
        void UpdateStock(int p_orderId, Orders p_order);

        /// <summary>
        /// This method will return a list of Orders that belong to either a customer or a Storefront
        /// </summary>
        /// <param name="p_customer_or_store"> can be either "store" or "customer"</param>
        /// <param name="p_id"> the matching ID corresponding to either the store or customer to find the list of orders from.</param>
        /// <returns> returns a list of Models.Orders </returns>
        List<Orders> GetOrdersList(string p_customer_or_store, int p_id);

        /// <summary>
        /// Will search the line_item_order many-many table and for each order_id, 
        /// we will add the LineItemIDs to that list of LineItems inside the Orders.
        /// </summary>
        /// <param name="p_order"> The Order that needs to be searched and have LineItemId's added. </param>
        /// <returns> The original Order with the correct line_item_id for each li in the List<LineItems>. </returns>
        List<Orders> AddLineItemsListToOrdersList(List<Orders> p_orderList);

        /// <summary>
        /// This Method will update the stock of a given LineItem (p_lineItemId) to the quantity provided (p_quantity)
        /// </summary>
        /// /// <param name="p_lineItemId"> this is the ID for the LineItem that will be updated. </param>
        /// <param name="p_quantity"> This is the quantity that the LineItem will be updated to. </param>
        void RefreshStock(int p_lineItemId, int p_quantity);

        /// <summary>
        /// Will update any values on a given customer. p_customer will contain the customer_id needed to locate the customer to update.
        /// </summary>
        /// <param name="p_customer"> the new values and id for the customer to update.</param>
        void UpdateCustomer(Models.Customer p_customer);
    }
}