using System;
using System.Globalization;
using Business_Logic;

namespace User_Interface
{
    public class AddCustomer : IMenu
    {
        private ICustomerBL _customerBL;
        public AddCustomer(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }
        public void Menu()
        {
            Console.WriteLine("Creating a new Account"+
                            "\n-------------------------"+
                            $" \nName - {SingletonCustomer.customer.Name}"+
                            $" \nAddress - {SingletonCustomer.customer.Address}"+
                            $" \nEmail - {SingletonCustomer.customer.Email}"+
                            $" \nPhone - {SingletonCustomer.customer.PhoneNumber}"+
                            "  \n-------------------------"+
                            "\n   [1] - Edit Name"+
                            "\n   [2] - Edit Address"+
                            "\n   [3] - Edit Email"+
                            "\n   [4] - Edit Phone Number"+
                            "\n   [5] - Save Account Details"+
                            "\n\n   [0] - Go Back");
        }
        public MenuType UserChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("   Type in the new value for the Name");
                    try
                    {
                        SingletonCustomer.customer.Name = Console.ReadLine().Trim().ToLower();
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}" +
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    return MenuType.AddCustomer;

                case "2":
                    Console.WriteLine("   Type in the new value for the Address");
                    try
                    {
                        SingletonCustomer.customer.Address = Console.ReadLine().Trim().ToLower();
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}" +
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    return MenuType.AddCustomer;

                case "3":
                    Console.WriteLine("   Type in the new value for the Email");
                    try
                    {
                        SingletonCustomer.customer.Email = Console.ReadLine().Trim().ToLower();
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}" +
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    return MenuType.AddCustomer;

                case "4":
                    Console.WriteLine("   Type in the new value for Phone Number");
                    try
                    {
                        SingletonCustomer.customer.PhoneNumber = Console.ReadLine().Trim();
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}" +
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    return MenuType.AddCustomer;

                case "5":
                    try
                    {
                        _customerBL.AddCustomer(SingletonCustomer.customer);
                    }
                    catch (System.Exception exception)
                    {
                        Console.WriteLine($"   {exception.Message}"+
                                        "\n   Press Enter to continue");
                        Console.ReadLine();
                        return MenuType.AddCustomer;
                    }
                    // retrieves the rest of the information for the customer that was just added. ie: customer_id
                    SingletonCustomer.customer = _customerBL.GetSingleCustomer(SingletonCustomer.customer.Name, SingletonCustomer.customer.Email);
                    Console.WriteLine($"   {SingletonCustomer.customer.Name} has been added to our list of customers. \n   Please press enter to continue.");
                    Console.ReadLine();
                    return MenuType.MainMenu;

                case "0":
                    return MenuType.StartMenu;

                default:
                    Console.WriteLine("   Please input a valid response!"+
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddCustomer;
            }
        }
    }
}
