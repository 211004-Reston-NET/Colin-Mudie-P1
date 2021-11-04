using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Models;
using Entity = Data_Access_Logic.Entities;

namespace Data_Access_Logic
{
    public class RepositoryCloud : IRepository
    {
        private Entity.MMDBContext _context;
        public RepositoryCloud(Entity.MMDBContext p_context)
        {
            _context = p_context;
        }

        public Models.Customer AddCustomer(Models.Customer p_customer)
        {
            _context.Customers.Add(
                new Entity.Customer()
                {
                    Name = p_customer.Name,
                    Email = p_customer.Email,
                    PhoneNumber = p_customer.PhoneNumber,
                    Address = p_customer.Address
                }
            );
            _context.SaveChanges();
            return p_customer;
        }

        public List<Models.Orders> AddLineItemsListToOrdersList(List<Orders> p_orderList)
        {   
            for (int i = 0; i < p_orderList.Count; i++)
            {
                p_orderList[i].LineItems = _context.LineItemOrders
                    .Where(lio => lio.OrderId == p_orderList[i].OrderId)
                    .Select(lio => 
                        new Models.LineItems(){
                            LineItemId = lio.LineItemId
                        }
                    ).ToList();
                for (int j = 0; j < p_orderList[i].LineItems.Count; j++)
                {
                    var tempLine = _context.LineItems
                        .FirstOrDefault<Entity.LineItem>(item => item.LineItemId == p_orderList[i].LineItems[j].LineItemId);
                    p_orderList[i].LineItems[j].ProductId = tempLine.ProductId;
                    p_orderList[i].LineItems[j].Quantity = tempLine.Quantity;
                    p_orderList[i].LineItems[j].StoreFrontId = tempLine.StorefrontId;
                    p_orderList[i].LineItems[j].Product = GetProductByProductId(p_orderList[i].LineItems[j].ProductId.GetValueOrDefault());
                }
            }
            return p_orderList;
        }

        public List<Models.Products> GetAllProducts()
        {
            return _context.Products.Select(prod => 
            new Models.Products()
            {
                Name = prod.Name,
                Price = prod.Price,
                Description = prod.Description,
                Brand = prod.Brand,
                Category = prod.Category,
                ProductId = prod.ProductId
            }
            ).ToList();
        }

        public List<Models.Customer> GetCustomerList()
        {
            return _context.Customers.Select(cust => 
            new Models.Customer()
            {
                Name = cust.Name,
                Email = cust.Email,
                Address = cust.Address,
                PhoneNumber = cust.PhoneNumber,
                CustomerId = cust.CustomerId,
                Orders = cust.Orders.Select(order => new Models.Orders(){
                    OrderId = order.OrderId,
                    Address = order.Address,
                    TotalPrice = order.TotalPrice
                }).ToList()
            }
            ).ToList();
        }

        public int GetLastOrderId()
        {
            int lastOrderId = 0;
            var listOfOrders = _context.Orders.Select(order => 
                new Models.Orders 
                {
                    OrderId = order.OrderId,
                }
            ).ToList();
            foreach(Models.Orders ord in listOfOrders)
            {
                if (ord.OrderId > lastOrderId){
                    lastOrderId = ord.OrderId;
                }
            }
            return lastOrderId;
        }
        public List<Models.LineItems> GetLineItemsList(int p_storeId)
        {
            return _context.LineItems
            .Where(item => item.Storefront.StorefrontId == p_storeId)
            .Select(item => 
                new Models.LineItems()
                {
                    Product = new Models.Products(){
                        Name = item.Product.Name,
                        Price = item.Product.Price,
                        Description = item.Product.Description,
                        Brand = item.Product.Brand,
                        Category = item.Product.Category,
                        ProductId = item.Product.ProductId
                    },
                    Quantity = item.Quantity,
                    LineItemId = item.LineItemId,
                    StoreFrontId = item.StorefrontId
                }
            ).ToList();
        }

        public List<Orders> GetOrdersList(string p_customer_or_store, int p_id)
        {
            List<Models.Orders> listOfOrders = new List<Orders>();
            switch (p_customer_or_store)
            {
                case "store":
                    return _context.Orders
                    .Where(order => order.StorefrontId == p_id)
                    .Select(order => 
                        new Models.Orders()
                        {
                            Address = order.Address,
                            OrderId = order.OrderId,
                            StoreFrontId = order.StorefrontId,
                            CustomerId = order.CustomerId,
                            TotalPrice = order.TotalPrice
                            }
                        ).ToList();
                case "customer":
                    return _context.Orders
                    .Where(order => order.CustomerId == p_id)
                    .Select(order =>
                        new Models.Orders()
                        {
                            Address = order.Address,
                            OrderId = order.OrderId,
                            StoreFrontId = order.StorefrontId,
                            CustomerId = order.CustomerId,
                            TotalPrice = order.TotalPrice
                        }
                    ).ToList();
                default:
                    return null;
            }
        }

        public Models.Products GetProductByProductId(int p_productId)
        {
            var result = _context.Products
                .FirstOrDefault<Entity.Product>(prod => prod.ProductId == p_productId);
            return new Models.Products()
                {
                    Name = result.Name,
                    Price = result.Price,
                    Description = result.Description,
                    Brand = result.Brand,
                    Category = result.Category,
                    ProductId = result.ProductId
                };
            
        }

        public List<Models.StoreFront> GetStoreFrontList()
        {
            return _context.Storefronts.Select(store => 
                // converting Entities Storefront to Models.StoreFront
                new Models.StoreFront()
                {
                    Name = store.Name,
                    Address = store.Address,
                        // StoreFront.LineItems is a list of LineItems, in order to convert a list of Entities.LineItems to Models.
                    LineItems = store.LineItems.Select(item => new Models.LineItems(){
                        Quantity = item.Quantity,
                        Product = new Models.Products(){
                            Name = item.Product.Name,
                            Price = item.Product.Price,
                            Description = item.Product.Description,
                            Brand = item.Product.Brand,
                            Category = item.Product.Category,
                            ProductId = item.Product.ProductId
                        }, 
                        LineItemId = item.LineItemId
                    }).ToList(),
                        // 
                    Orders = store.Orders.Select(order => new Models.Orders(){
                        OrderId = order.OrderId,
                        Address = order.Address,
                        TotalPrice = order.TotalPrice
                    }).ToList(),
                    StoreFrontId = store.StorefrontId
                }
            ).ToList();
        }

        public Models.Orders PlaceOrder(Models.Customer p_customer, Models.Orders p_order)
        {
            // add order to customer's list of orders
            var customer = _context.Customers
                                .First<Entity.Customer>(cust => cust.CustomerId == p_customer.CustomerId);
            customer.Orders.Add(new Entity.Order()
                {
                    Address = p_order.Address,
                    TotalPrice = p_order.TotalPrice,
                    StorefrontId = p_order.StoreFrontId,
                    CustomerId = p_order.CustomerId,
                });
            _context.SaveChanges();
            int lastOrderId = GetLastOrderId();
            UpdateStock(lastOrderId, p_order);
            
            return p_order;
        }

        public void RefreshStock(int p_lineItemId, int p_quantity)
        {
            var query = _context.LineItems
                .FirstOrDefault<Entity.LineItem>(dbItem => dbItem.LineItemId == p_lineItemId);
            query.Quantity = p_quantity;
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer p_customer)
        {
            var query = _context.Customers
                            .FirstOrDefault(cust => cust.CustomerId == p_customer.CustomerId);
            query.Name = p_customer.Name;
            query.Address = p_customer.Address;
            query.Email = p_customer.Email;
            query.PhoneNumber = p_customer.PhoneNumber;
            _context.SaveChanges();
        }

        public void UpdateStock(int p_orderId, Models.Orders p_order)
        {

            foreach (Models.LineItems item in p_order.LineItems)
            {
                // add each item to the order using line_item_order
                _context.LineItemOrders.Add(new Entity.LineItemOrder()
                {
                    LineItemId = item.LineItemId,
                    OrderId = p_orderId
                });

                // update stock in correct storefront's lineItems
                var stockUpdate = _context.LineItems.FirstOrDefault<Entity.LineItem>(dbItem => dbItem.LineItemId == item.LineItemId);
                stockUpdate.Quantity = stockUpdate.Quantity - item.Quantity;
            }

            //save these changes
            _context.SaveChanges();
        }
    }
}