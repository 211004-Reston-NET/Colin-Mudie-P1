using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
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
            //FIX THIS

            // for (int i = 0; i < p_orderList.Count; i++)
            // {
            //     p_orderList[i].LineItems = _context.LineItemOrders
            //         .Where(lio => lio.OrderId == p_orderList[i].OrderId)
            //         .Select(lio => 
            //             new LineItems(){
            //                 LineItemsId = lio.LineItemsId
            //             }
            //         ).ToList();
            //     for (int j = 0; j < p_orderList[i].LineItems.Count; j++)
            //     {
            //         var tempLine = _context.LineItems
            //                     .FirstOrDefault<LineItems>(item => item.LineItemsId == p_orderList[i].LineItems[j].LineItemsId);
            //         p_orderList[i].LineItems[j] = tempLine;
            //     }
            // }
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
            var listOfOrders = _context.Orders.ToList();
            foreach (Order ord in listOfOrders)
            {
                if (ord.OrderId > lastOrderId)
                {
                    lastOrderId = ord.OrderId;
                }
            }
            return lastOrderId;
        }

        public LineItems GetLineItemsById(int p_lineItemId)
        {
            return _context.LineItems
                            .AsNoTracking()
                            .Select(item =>
                            new LineItems()
                            {
                                Product = new Product()
                                {
                                    Name = item.Product.Name,
                                    Price = item.Product.Price,
                                    Description = item.Product.Description,
                                    Brand = item.Product.Brand,
                                    Category = item.Product.Category,
                                    ProductId = item.Product.ProductId
                                },
                                Quantity = item.Quantity,
                                LineItemsId = item.LineItemsId,
                                StoreFrontId = item.StoreFrontId,
                                ProductId = item.ProductId
                            })
                        .ToList()
                        
                        .FirstOrDefault(item => item.LineItemsId == p_lineItemId);
        }

        public List<LineItems> GetLineItemsList(int p_storeId)
        {
            return _context.LineItems
                        .Where(item => item.StoreFront.StoreFrontId == p_storeId)
                        .AsNoTracking()
                        .Select(item =>
                            new LineItems()
                            {
                                Product = new Product()
                                {
                                    Name = item.Product.Name,
                                    Price = item.Product.Price,
                                    Description = item.Product.Description,
                                    Brand = item.Product.Brand,
                                    Category = item.Product.Category,
                                    ProductId = item.Product.ProductId
                                },
                                Quantity = item.Quantity,
                                LineItemsId = item.LineItemsId,
                                StoreFrontId = item.StoreFrontId,
                                ProductId = item.ProductId
                            }
                        ).ToList();

        }

        public List<Order> GetOrdersListForStore(int p_storeId)
        {
            return _context.Orders
                .AsNoTracking()
                .Where(order => order.StoreFrontId == p_storeId).ToList();
        }

        public List<Order> GetOrdersListForCustomer(string p_custId)
        {
            return _context.Orders
                .AsNoTracking()
                .Where(order => order.CustomerId == p_custId).ToList();
        }

        public Product GetProductByProductId(int p_productId)
        {
            return _context.Products.FirstOrDefault<Product>(prod => prod.ProductId == p_productId);
        }

        public StoreFront GetStoreFrontById(int p_storeId)
        {
            return _context.Storefronts
                        .Select(store =>
                            new StoreFront()
                            {
                                Name = store.Name,
                                Address = store.Address,
                                StoreFrontId = store.StoreFrontId
                            }
                        )
                    .AsNoTracking()
                    .FirstOrDefault(store => store.StoreFrontId == p_storeId);
        }

        public List<StoreFront> GetStoreFrontList()
        {
            return _context.Storefronts.ToList();
        }

        public void PlaceOrder(Order p_order)
        {
            var customer = _context.Customers
                                        //.AsNoTracking()
                                        .FirstOrDefault(cust => cust.Id == p_order.CustomerId);
            
            customer.Order.Add(p_order);
            _context.SaveChanges();
        }

        public void RefreshStock(LineItems p_lineItem)
        {
            _context.LineItems.Update(p_lineItem);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer p_customer)
        {
            var query = _context.Customers
                            .FirstOrDefault(cust => cust.Id == p_customer.Id);
            query = p_customer;
            _context.SaveChanges();
        }

        public void UpdateStock(int p_orderId, Order p_order)
        {
            foreach (LineItems item in p_order.LineItems)
            {
                var stockUpdate = _context.LineItems.FirstOrDefault<LineItems>(dbItem => dbItem.LineItemsId == item.LineItemsId);
                stockUpdate.Quantity = stockUpdate.Quantity - item.Quantity;
            }
            //save these changes
            _context.SaveChanges();
        }

        public Order GetOrderById(int p_orderId)
        {
            return _context.Orders
                        .Include("LineItems")
                        .Include("Customer")
                        .AsNoTracking()
                        .FirstOrDefault(ord => ord.OrderId == p_orderId);
        }
    }
}