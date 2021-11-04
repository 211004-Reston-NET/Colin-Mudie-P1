using System.Collections.Generic;
using Data_Access_Logic;
using Models;

namespace Business_Logic
{
    public class OrderBL : IOrderBL
    {
        private IRepository _repo;

        public OrderBL(IRepository p_repo)
        {
            _repo = p_repo;
        }
        public List<Orders> GetOrdersList(string p_customer_or_store, int p_id)
        {   
            List<Orders> listOfOrders = _repo.GetOrdersList(p_customer_or_store, p_id);
            listOfOrders = _repo.AddLineItemsListToOrdersList(listOfOrders);
            return listOfOrders;
        }
    }
}