using System.Collections.Generic;
using System.Linq;
using Data_Access_Logic;
using Models;

namespace Business_Logic
{
    public class LineItemsBL : ILineItemsBL
    {
        private IRepository _repo;

        public LineItemsBL(IRepository p_repo)
        {
            _repo = p_repo;
        }


        public List<LineItems> GetLineItems(int p_storeId)
        {
            return _repo.GetLineItemsList(p_storeId);
        }

        public LineItems GetLineItemsById(int p_lineItemId)
        {
            return _repo.GetLineItemsById(p_lineItemId);
        }

        public LineItems GetLineItemSearch(string p_lineItemName, int p_storeId)
        {
            List<LineItems> lines = _repo.GetLineItemsList(p_storeId);
            if (lines == null)
            {
                throw new System.Exception($"Could not find any Items from the Store ID {p_storeId}");
            }
            List<LineItems> result = lines.Where(item => item.Product.Name.ToLower().Contains(p_lineItemName.ToLower())).ToList();
            return result[0];
        }

        public void RefreshStock(LineItems p_lineItem)
        {
            _repo.RefreshStock(p_lineItem);
        }

        
    }
}