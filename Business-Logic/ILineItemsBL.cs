using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface ILineItemsBL
    {
        /// <summary>
        /// This will return a list of Products stored in the db.
        /// </summary>
        /// <param name="p_storeId"> the parameter passed in will be the stores ID, in order to determine which product list to use.</param>
        /// <returns>It will return a list of Products.</returns>
        List<LineItems> GetLineItems(int p_storeId);

        /// <summary>
        /// This Method will update the stock of a given LineItem (p_lineItemId) to the quantity provided (p_quantity)
        /// </summary>
        /// /// <param name="p_lineItemId"> this is the ID for the LineItem that will be updated. </param>
        /// <param name="p_quantity"> This is the quantity that the LineItem will be updated to. </param>
        void RefreshStock(int p_lineItemId, int p_quantity);

        /// <summary>
        /// This method will take a search parameter from a user and locate a line Item from 
        /// the given storeId that contains the name searched for.
        /// </summary>
        /// <param name="p_lineItemName"> The name of the LineItem.Product.Name that will be searched for. </param>
        /// <param name="p_storeId"> The storefront_id to search li's at. </param>
        /// <returns></returns>
        LineItems GetLineItemSearch(string p_lineItemName, int p_storeId); 
    }
}