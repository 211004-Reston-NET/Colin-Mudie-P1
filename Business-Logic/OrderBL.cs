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

        public Order GetOrderById(int p_orderId)
        {
            return _repo.GetOrderById(p_orderId);
        }

        public List<Order> GetOrdersListForCustomer(string p_customerId)
        {
            List<Order> listOfOrders = _repo.GetOrdersListForCustomer(p_customerId);
            listOfOrders = _repo.AddLineItemsListToOrdersList(listOfOrders);
            return listOfOrders;
        }

        public List<Order> GetOrdersListForStore(int p_storeId)
        {
            List<Order> listOfOrders = _repo.GetOrdersListForStore(p_storeId);
            listOfOrders = _repo.AddLineItemsListToOrdersList(listOfOrders);
            return listOfOrders;
        }

        public void PlaceOrder(Order p_order)
        {
            _repo.PlaceOrder(p_order);
        }
    }
}