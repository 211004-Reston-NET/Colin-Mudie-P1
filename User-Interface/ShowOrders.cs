using System;
using System.Collections.Generic;
using Business_Logic;
using Models;

namespace User_Interface
{
    public class ShowOrders : IMenu
    {
        private IOrderBL  _orderBL;
        public ShowOrders(IOrderBL p_orderBL)
        {
            _orderBL = p_orderBL;
        }
        
        public void Menu()
        {
            List<Orders> listOfOrders = _orderBL.GetOrdersList(SingletonCustomer.orderType, SingletonCustomer.storeOrCustID);
            int _count = 0;
            if (SingletonCustomer.orderType == "customer" ){
                Console.WriteLine($"We have found {listOfOrders.Count} orders from {SingletonCustomer.customer.Name}" +
                            "\n-------------------------");
            } else {
                Console.WriteLine($"We have found {listOfOrders.Count} orders from {SingletonCustomer.location}" +
                            "\n-------------------------");
            }
            foreach (Orders ord in listOfOrders)
            {
                _count++;
                Console.WriteLine($"\n   Order {_count}");
                Console.WriteLine(ord);
                Console.WriteLine("------------------------- \n ");
                
            }
            Console.WriteLine("\n   [0] - Go back to Main Menu");
        }

        public MenuType UserChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    return MenuType.PlaceOrder;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("   Please input a valid response!" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.MainMenu;
            }
        }
    }
}
