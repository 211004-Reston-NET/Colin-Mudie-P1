using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface ICustomerBL
    {
        /// <summary>
        ///     This will return a list of Customers stored in the database
        /// </summary>
        /// <returns>It will return a list of customers</returns>
        List<Customer> GetCustomerList();

        /// <summary>
        ///     Will check the database for a customer with a name and email we give it. 
        ///     then return the 1st customer if finds with that information.
        /// </summary>
        /// <param name="p_name"> customer name to search for </param>
        /// <param name="p_email"> customer email to search for </param>
        /// <returns> returns the customer from the database</returns>
        Customer GetSingleCustomer(string p_name, string p_email);

        /// <summary>
        ///     Adds a customer to the database
        /// </summary>
        /// <param name="p_customer"> This is the customer to add</param>
        /// <returns>returns the added customer</returns>
        Customer AddCustomer(Customer p_customer);

        /// <summary>
        /// Will update any values on a given customer. p_customer will contain the customer_id needed to locate the customer to update.
        /// </summary>
        /// <param name="p_customer"> the new values and id for the customer to update.</param>
        void UpdateCustomer(Models.Customer p_customer);
    }
}