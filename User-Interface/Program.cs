using System;
using Models;
using Business_Logic;
using Data_Access_Logic;
using System.Globalization;

namespace User_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            bool stillOn = true;
            IFactory factory = new MenuFactory();
            IMenu page = factory.GetMenu(MenuType.StartMenu);
            while (stillOn)
            {
                    // Store Manager ASCII Art
                AscArt();
                if (SingletonCustomer.customer.Name != null)
                {
                    Console.WriteLine($"                               - Current Customer: {SingletonCustomer.customer.Name}");
                }
                if (SingletonCustomer.location != null)
                {
                    Console.WriteLine($"                               - Current Store Location: {SingletonCustomer.location}");
                }
                page.Menu();
                MenuType currentPage = page.UserChoice();
                if (currentPage == MenuType.Exit)
                {
                    stillOn = false;
                    AscArt();
                    Console.WriteLine("   Now Exiting," +
                                    "\n   Thank you for using the Store Manager!");
                }
                else
                {
                    page = factory.GetMenu(currentPage);
                }
            }
        }
        public static void AscArt(){
            // Store Manager ASCII Art String
            Console.Clear();
            Console.WriteLine("    __  ___ _        __     _                         __  ___            __        __            " +
            "\n   /  |/  /(_)_____ / /_   (_)____ _ ____ _ ____     /  |/  /____   ____/ /__  __ / /____ _ _____" +
            "\n  / /|_/ // // ___// __ \\ / // __ `// __ `// __ \\   / /|_/ // __ \\ / __  // / / // // __ `// ___/" +
            "\n / /  / // // /__ / / / // // /_/ // /_/ // / / /  / /  / // /_/ // /_/ // /_/ // // /_/ // /    " +
            "\n/_/  /_//_/ \\___//_/ /_//_/ \\__, / \\__,_//_/ /_/  /_/  /_/ \\____/ \\__,_/ \\__,_//_/ \\__,_//_/     " +
            "\n                           /____/                                                                ");
            
        }
    }
}
