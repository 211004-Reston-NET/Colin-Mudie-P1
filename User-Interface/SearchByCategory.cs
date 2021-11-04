using System;
using System.Collections.Generic;
using Business_Logic;
using Models;

namespace User_Interface
{
    public class SearchByCategory : IMenu
    {
        private IProductBL _productBL;
        public SearchByCategory(IProductBL p_productBL)
        {
            _productBL = p_productBL;
        }
        private static string _search = "none";
        public void Menu()
        {
            Console.WriteLine($"View Products by Category." +
                            $"\n   Current Category: {_search}" +
                            "\n-------------------------");
            List<Products> listOfProducts = _productBL.SearchByCategory(_search);

            foreach (Products prod in listOfProducts)
            {
                Console.WriteLine(prod);
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("\n   [1] - Enter New Search Category" +
                            "\n   [2] - View Popular Categories"+
                            "\n   [0] - Go back");
        }

        public MenuType UserChoice()
        {
            string userChoice = Console.ReadLine();
            switch (userChoice)
            {
                case "1":
                    Console.WriteLine("   Please input the category name");
                    _search = Console.ReadLine().Trim().ToLower();
                    return MenuType.SearchByCategory;
                case "2":
                    Console.WriteLine("   Popular Eurorack Categories" +
                                    "\n-------------------------" +
                                    "\n   Clock," +
                                    "\n   Envelope Generator," +
                                    "\n   Oscillator," +
                                    "\n   Sampler," +
                                    "\n   Sequencer" +
                                    "\n\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchByCategory;
                case "0":
                    return MenuType.MainMenu;
                default:
                    Console.WriteLine("   Please input a valid response!" +
                                    "\n   Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchByCategory;
            }
        }
    }
}