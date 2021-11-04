using System;
using System.Collections.Generic;
using Business_Logic;
using System.Linq;
using Models;

namespace User_Interface
{
    public class PlaceOrder : IMenu
    {
        private ILineItemsBL _lineItems;
        private ICustomerBL _customerBL;
        public PlaceOrder(ICustomerBL p_customerBL, ILineItemsBL p_lineItems)
        {
            _customerBL = p_customerBL;
            _lineItems = p_lineItems;
        }
        public void Menu()
        {
        
                List<LineItems> listOfLineItems = _lineItems.GetLineItems(SingletonCustomer.orders.StoreFrontId);
            
            
            Console.WriteLine($"Current List of Products from {SingletonCustomer.location}");
            
            foreach (LineItems prod in listOfLineItems)
            {
                Console.WriteLine("-------------------------" +
                                $" \nBrand: {prod.Product.Brand}" +
                                $" \nName: {prod.Product.Name}" +
                                $" \nPrice: {prod.Product.Price}" +
                                $" \nStock Left: {prod.Quantity}");
            }
            Console.WriteLine("\n_________________________" +
                            "\n      Shopping Cart" +
                            "\n-------------------------");
            if (SingletonCustomer.orders.LineItems.Count == 0)
            {
                Console.WriteLine("          empty" +
                                "\n-------------------------");
            }
            foreach (LineItems item in SingletonCustomer.orders.LineItems)
            {
                Console.WriteLine($"   {item.Product.Name} "+
                                $" \n   Price: {item.Product.Price}" +
                                "\n-------------------------");
            }
            Console.WriteLine($"Store Location: {SingletonCustomer.location}" +
                            $" \nTotal Price: {SingletonCustomer.orders.TotalPrice}" +
                            "\n-------------------------" +
                            "\n   [1] - Add Product to Shopping Cart" +
                            "\n   [2] - Empty Shopping Cart"+
                            "\n   [3] - Complete Order" +
                            "\n\n   [0] - Main Menu");
        }
        public MenuType UserChoice()
        {
            List<LineItems> listOfLineItems = _lineItems.GetLineItems(SingletonCustomer.orders.StoreFrontId);
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("   Please Type the name of the product you would like to add.");
                    string _inputName = Console.ReadLine().Trim().ToLower();
                    LineItems lineItemsSearch = _lineItems.GetLineItemSearch(_inputName, SingletonCustomer.orders.StoreFrontId);
                    Console.WriteLine($"   Is this correct Module: {lineItemsSearch.Product.Name}"+
                                        "\n   [1] - Yes"+
                                        "\n   [2] - No");
                    string confirmPick = Console.ReadLine();
                    if (confirmPick == "1")
                    {
                        Console.WriteLine($"   How many {lineItemsSearch.Product.Name} module's would you like to add?");
                        try
                        {
                            int _inputQuantity = int.Parse(Console.ReadLine().Trim());
                            if (_inputQuantity <= 0)
                            {
                                Console.WriteLine($"   You must enter a quantity higher than 0" +
                                                "\n   Press Enter to continue");
                                Console.ReadLine();
                                return MenuType.PlaceOrder;
                            }
                            else if (_inputQuantity == 1)
                            {
                                SingletonCustomer.orders.LineItems.Add(lineItemsSearch);
                                SingletonCustomer.orders.TotalPrice += (_inputQuantity * lineItemsSearch.Product.Price);
                                Console.WriteLine($"   {_inputQuantity} {lineItemsSearch.Product.Name} module has been added to the Shopping Cart" +
                                                "\n   Press Enter to continue");
                                Console.ReadLine();
                            }
                            else
                            {
                                for (int i = 0; i < _inputQuantity; i++)
                                {
                                    SingletonCustomer.orders.LineItems.Add(lineItemsSearch);
                                }
                                SingletonCustomer.orders.TotalPrice += (_inputQuantity * lineItemsSearch.Product.Price);
                                Console.WriteLine($"   {_inputQuantity} {lineItemsSearch.Product.Name} module's have been added to the Shopping Cart" +
                                                "\n   Press Enter to continue");
                                Console.ReadLine();
                            }
                        }
                        catch (System.FormatException)
                        {
                            Console.WriteLine("   Please input a number!" +
                                    "\n   Press Enter to continue");
                            Console.ReadLine();
                            return MenuType.PlaceOrder;
                        }
                        
                    }
                    return MenuType.PlaceOrder;

                case "2":
                    SingletonCustomer.orders.LineItems.Clear();
                    SingletonCustomer.orders.TotalPrice = 0;
                    return MenuType.PlaceOrder;

                case "3":
                    //--------- add Order to DB here.---------\\
                    SingletonCustomer.orders.CustomerId = SingletonCustomer.customer.CustomerId;
                    _customerBL.PlaceOrder(SingletonCustomer.customer, SingletonCustomer.orders);
                    
                    Console.WriteLine("   Order Placed" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MainMenu;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("   Please input a valid response!" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.PlaceOrder;
            }
        }
    }
}