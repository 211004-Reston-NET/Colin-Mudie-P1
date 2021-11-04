using System.Collections.Generic;
using Models;

namespace Business_Logic
{
    public interface IProductBL
    {
        /// <summary>
        /// Will grab the list of products from Repository.GetAllProducts() and then usig LINQ,
        /// it will look for products in the list that contain a category containing p_category.
        /// </summary>
        /// <param name="p_category"> the category that will be searched for. </param>
        /// <returns> returns a list of products only containing a Category matching p_category. </returns>
        List<Products> SearchByCategory(string p_category);
    }
}