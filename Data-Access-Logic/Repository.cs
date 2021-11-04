using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Models;

namespace Data_Access_Logic
{
public class Repository
    // public class Repository : IRepository
    {
        private const string _filepath = "./../Data-Access-Logic/Database/";
        private string _jsonString;
        public Customer AddCustomer(Customer p_customer)
        {
            List<Customer> listOfCustomers = GetCustomerList();
            listOfCustomers.Add(p_customer);
            _jsonString = JsonSerializer.Serialize(listOfCustomers, new JsonSerializerOptions { WriteIndented = true});

            File.WriteAllText(_filepath+"Customer.json", _jsonString);
            return p_customer;
        }

        public List<Orders> AddLineItemsListToOrdersList(List<Orders> p_order)
        {
            throw new NotImplementedException();
        }

        public List<LineItems> ChangeLineItemsQuantity(List<LineItems> p_lineItems, string p_location)
        {
            _jsonString = JsonSerializer.Serialize(p_lineItems, new JsonSerializerOptions { WriteIndented = true });
            switch (p_location)
            {
                case "Royal Oak":
                    File.WriteAllText(_filepath + "RoyalOakProducts.json", _jsonString);
                    break;
                case "Mt Pleasant":
                    File.WriteAllText(_filepath + "MtPleasantProducts.json", _jsonString);
                    break;
                default:
                    File.WriteAllText(_filepath + "MtPleasantProducts.json", _jsonString);
                    break;
            }
    return p_lineItems;
        }

        public List<Products> GetAllProducts()
        {
            _jsonString = File.ReadAllText(_filepath+"Products.json");
            return JsonSerializer.Deserialize<List<Products>>(_jsonString);
        }

        public List<Customer> GetCustomerList()
        {
            _jsonString = File.ReadAllText(_filepath+"Customer.json");
            return JsonSerializer.Deserialize<List<Customer>>(_jsonString);
        }

        public int GetLastOrderId()
        {
            throw new NotImplementedException();
        }

        public List<LineItems> GetLineItemsList(int p_storeId)
        {
            switch (p_storeId)
            {
                case 1:
                    _jsonString = File.ReadAllText(_filepath+"RoyalOakProducts.json");
                    break;
                case 2:
                    _jsonString = File.ReadAllText(_filepath+"MtPleasantProducts.json");
                    break;
                default:
                    _jsonString = File.ReadAllText(_filepath+"MtPleasantProducts.json");
                break;
            }
            
            return JsonSerializer.Deserialize<List<LineItems>>(_jsonString);
        }

        public List<Orders> GetOrdersList(string p_cust_or_store, int p_id)
        {
            throw new NotImplementedException();
        }

        public Products GetProductByProductId(int p_productId)
        {
            throw new NotImplementedException();
        }

        public List<StoreFront> GetStoreFrontList()
        {
            _jsonString = File.ReadAllText(_filepath+"StoreFront.json");
            return JsonSerializer.Deserialize<List<StoreFront>>(_jsonString);
        }

        public Orders PlaceOrder(Customer p_customer, Orders p_order)
        {
            List<Customer> listOfCustomers = GetCustomerList();
            foreach (Customer customer in listOfCustomers)
            {
                if (customer.Name == p_customer.Name && 
                    customer.Address == p_customer.Address && 
                    customer.Email == p_customer.Email && 
                    customer.PhoneNumber == p_customer.PhoneNumber)
                {
                    customer.Orders.Add(p_order);
                    _jsonString = JsonSerializer.Serialize(listOfCustomers, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(_filepath + "Customer.json", _jsonString);
                }
            }
            return p_order;
        }

        public void UpdateStock(int p_orderId, Orders p_order)
        {
            throw new NotImplementedException();
        }
    }
}
