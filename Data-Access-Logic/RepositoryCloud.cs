using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Models;

namespace Data_Access_Logic
{
    public class RepositoryCloud : IRepository
    {
        private MMDBContext _context;
        public RepositoryCloud(MMDBContext p_context)
        {
            _context = p_context;
        }

        public Customer AddCustomer(Customer p_customer)
        {
            _context.Customers.Add(p_customer);
            _context.SaveChanges();
            return p_customer;
        }

        public List<Order> AddLineItemsListToOrdersList(List<Order> p_orderList)
        {   
            for (int i = 0; i < p_orderList.Count; i++)
            {
                p_orderList[i].LineItems = _context.LineItemOrders
                    .Where(lio => lio.OrderId == p_orderList[i].OrderId)
                    .Select(lio => 
                        new LineItems(){
                            LineItemsId = lio.LineItemsId
                        }
                    ).ToList();
                for (int j = 0; j < p_orderList[i].LineItems.Count; j++)
                {
                    var tempLine = _context.LineItems
                                .FirstOrDefault<LineItems>(item => item.LineItemsId == p_orderList[i].LineItems[j].LineItemsId);
                    p_orderList[i].LineItems[j] = tempLine;
                }
            }
            return p_orderList;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public List<Customer> GetCustomerList()
        {
            return _context.Customers.ToList();
        }

        public int GetLastOrderId()
        {
            int lastOrderId = 0;
            var listOfOrders = _context.Orders.Select(order => 
                new Order
                {
                    OrderId = order.OrderId,
                }
            ).ToList();
            foreach(Order ord in listOfOrders)
            {
                if (ord.OrderId > lastOrderId){
                    lastOrderId = ord.OrderId;
                }
            }
            return lastOrderId;
        }
        public List<LineItems> GetLineItemsList(int p_storeId)
        {
            return _context.LineItems.Where(item => item.StoreFront.StoreFrontId == p_storeId).ToList();
        }

        public List<Order> GetOrdersList(string p_customer_or_store, int p_id)
        {
            List<Order> listOfOrders = new List<Order>();
            switch (p_customer_or_store)
            {
                case "store":
                    return _context.Orders
                    .Where(order => order.StoreFrontId == p_id).ToList();
                case "customer":
                    return _context.Orders
                    .Where(order => order.CustomerId == p_id).ToList();
                default:
                    return null;
            }
        }

        public Product GetProductByProductId(int p_productId)
        {
            return _context.Products.FirstOrDefault<Product>(prod => prod.ProductId == p_productId);            
        }

        public List<StoreFront> GetStoreFrontList()
        {
            return _context.Storefronts.ToList();
        }

        public Order PlaceOrder(Customer p_customer, Order p_order)
        {
            // add order to customer's list of orders
            var customer = _context.Customers
                                .First<Customer>(cust => cust.CustomerId == p_customer.CustomerId);
            customer.Order.Add(p_order);
            _context.SaveChanges();
            int lastOrderId = GetLastOrderId();
            UpdateStock(lastOrderId, p_order);
            return p_order;
        }

        public void RefreshStock(int p_lineItemId, int p_quantity)
        {
            var query = _context.LineItems
                            .FirstOrDefault<LineItems>(dbItem => dbItem.LineItemsId == p_lineItemId);
            query.Quantity = p_quantity;
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer p_customer)
        {
            var query = _context.Customers
                            .FirstOrDefault(cust => cust.CustomerId == p_customer.CustomerId);
            query = p_customer;
            _context.SaveChanges();
        }

        public void UpdateStock(int p_orderId, Order p_order)
        {
            foreach (LineItems item in p_order.LineItems)
            {
                // add each item to the order using line_item_order
                _context.LineItemOrders.Add(new LineItemOrder()
                {
                    LineItemsId = item.LineItemsId,
                    OrderId = p_orderId
                });
                // update stock in correct storefront's lineItems
                var stockUpdate = _context.LineItems.FirstOrDefault<LineItems>(dbItem => dbItem.LineItemsId == item.LineItemsId);
                stockUpdate.Quantity = stockUpdate.Quantity - item.Quantity;
            }
            //save these changes
            _context.SaveChanges();
        }
    }
}