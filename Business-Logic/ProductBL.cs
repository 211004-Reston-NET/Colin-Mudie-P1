using System.Collections.Generic;
using System.Linq;
using Data_Access_Logic;
using Models;

namespace Business_Logic
{
    public class ProductBL : IProductBL
    {
        private IRepository _repo;
        public ProductBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        public List<Products> SearchByCategory(string p_category)
        {
            List<Products> products = _repo.GetAllProducts();
            return products.Where(prod => prod.Category.ToLower().Contains(p_category.ToLower())).ToList();
        }
    }
}