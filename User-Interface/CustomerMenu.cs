using System;
using Business_Logic;

namespace User_Interface
{
    public class CustomerMenu : IMenu
    {
        private ICustomerBL _customerBL;
        public CustomerMenu(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }
        public void Menu()
        {
            Console.WriteLine("Edit Customer Information" +
                            "\n-------------------------" +
                            $" \nName - {SingletonCustomer.customer.Name}" +
                            $" \nAddress - {SingletonCustomer.customer.Address}" +
                            $" \nEmail - {SingletonCustomer.customer.Email}" +
                            $" \nPhone - {SingletonCustomer.customer.PhoneNumber}" +
                            $" \nPrevious Orders - {SingletonCustomer.customer.Orders.Count}"+
                            "  \n-------------------------" +
                            "\n   [1] - Edit Name" +
                            "\n   [2] - Edit Address" +
                            "\n   [3] - Edit Email" +
                            "\n   [4] - Edit Phone Number" +
                            "\n   [5] - Save Account Details" +
                            "\n   [6] - View Previous Orders" +
                            "\n\n   [0] - Go Back");
        }

        public MenuType UserChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("   Type in the new value for the Name");
                    SingletonCustomer.customer.Name = Console.ReadLine().Trim().ToLower();
                    return MenuType.CustomerMenu;

                case "2":
                    Console.WriteLine("   Type in the new value for the Address");
                    SingletonCustomer.customer.Address = Console.ReadLine().Trim().ToLower();
                    return MenuType.CustomerMenu;

                case "3":
                    Console.WriteLine("   Type in the new value for the Email");
                    SingletonCustomer.customer.Email = Console.ReadLine().Trim().ToLower();
                    return MenuType.CustomerMenu;

                case "4":
                    Console.WriteLine("   Type in the new value for Phone Number");
                    SingletonCustomer.customer.PhoneNumber = Console.ReadLine().Trim();
                    return MenuType.CustomerMenu;

                case "5":
                    try
                    {
                        _customerBL.UpdateCustomer(SingletonCustomer.customer);
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}" +
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.CustomerMenu;
                    }
                    SingletonCustomer.customer = _customerBL.GetSingleCustomer(SingletonCustomer.customer.Name, SingletonCustomer.customer.Email);
                    Console.WriteLine($"   {SingletonCustomer.customer.Name} has been added to our list of customers. \n   Please press enter to continue.");
                    Console.ReadLine();
                    return MenuType.MainMenu;
                case "6":
                    SingletonCustomer.orderType = "customer";
                    SingletonCustomer.storeOrCustID = SingletonCustomer.customer.CustomerId;
                    return MenuType.ViewOrderHistory;
                case "0":
                    return MenuType.MainMenu;

                default:
                    Console.WriteLine("   Please input a valid response!" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.CustomerMenu;
            }
        }
    }
}