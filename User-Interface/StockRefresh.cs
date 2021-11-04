using System;
using System.Collections.Generic;
using Business_Logic;
using Models;

namespace User_Interface
{
    public class StockRefresh : IMenu
    {
        private static List<LineItems> _stockList = new List<LineItems>();
        private ILineItemsBL _lineItems;
        public StockRefresh(ILineItemsBL p_lineItems)
        {
            _lineItems = p_lineItems;
            _stockList = _lineItems.GetLineItems(SingletonCustomer.orders.StoreFrontId);
        }
        
        public void Menu()
        {
            Console.WriteLine($"Current List of Products from {SingletonCustomer.location}" +
                            "\n-------------------------");
            foreach (LineItems prod in _stockList)
            {
                Console.WriteLine("-------------------------" +
                                $" \nBrand: {prod.Product.Brand}" +
                                $" \nName: {prod.Product.Name}" +
                                $" \nPrice: {prod.Product.Price}" +
                                $" \nStock Left: {prod.Quantity}");
            }
            Console.WriteLine($"\n \nStore Location: {SingletonCustomer.location}" +
                            "\n-------------------------" +
                            "\n   [1] - Edit Stock of Item" +
                            "\n   [2] - Choose a Different Store"+
                            "\n\n   [0] - Main Menu");
        }

        public MenuType UserChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("   Please Type the name of the product you would like to restock.");
                    string _inputName = Console.ReadLine().Trim().ToLower();
                    LineItems lineItemsSearch = _lineItems.GetLineItemSearch(_inputName, SingletonCustomer.orders.StoreFrontId);
                    Console.WriteLine($"   Is this correct Module: {lineItemsSearch.Product.Name}" +
                                        "\n   [1] - Yes" +
                                        "\n   [2] - No");
                    string confirmPick = Console.ReadLine();
                    if (confirmPick == "1")
                        {
                            Console.WriteLine($"   What should {_inputName} module's stock be updated to?");
                            try
                            {
                                int _inputQuantity = int.Parse(Console.ReadLine().Trim());
                                // edit db here.
                                _lineItems.RefreshStock(lineItemsSearch.LineItemsId, _inputQuantity);
                            }
                            catch (System.FormatException)
                            {
                                Console.WriteLine("   Please input a number!" +
                                "\n   Press Enter to continue");
                                Console.ReadLine();
                                return MenuType.StockRefresh;
                            }
                            
                        }
                    return MenuType.StockRefresh;

                case "2":
                    return MenuType.ShowStoreFronts;

                case "0":
                    return MenuType.MainMenu;

                default:
                    Console.WriteLine("   Please input a valid response!" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.StockRefresh;
            }
        }
    }
}