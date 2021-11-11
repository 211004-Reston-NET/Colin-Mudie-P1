using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface IStoreFrontBL
    {
        /// <summary>
        /// This will return a list of StoreFronts stored in the database
        /// </summary>
        /// <returns>It will return a list of StoreFronts</returns>
        List<StoreFront> GetStoreFrontList();

        /// <summary>
        /// Will find and return a single Storefront with an ID matching the p_storeId parameter.
        /// </summary>
        /// <param name="p_storeId"> The ID for the store to find </param>
        /// <returns> Will return a single storefront. </returns>
        StoreFront GetStoreFrontById(int p_storeId);
    }
}