namespace User_Interface
{
    public enum MenuType
    {   
        StartMenu,
        MainMenu,
        AddCustomer,
        ShowCustomers,
        SearchForCustomer,
        CustomerMenu,
        ShowStoreFronts,
        ShowLineItems,
        SearchByCategory,
        PlaceOrder,
        ViewOrderHistory,
        StockRefresh,
        Exit
    }
    public interface IMenu
    {
        /// <summary>
        /// Will display the menu of the current menu class 
        /// and the choices that will go along with them.
        /// </summary>
        void Menu();

        /// <summary>
        /// Will Record the users input choice to the change the menu
        /// based on the end-user's choice
        /// </summary>
        /// <returns> This method will return a menu that the user will go to next. </returns>
        MenuType UserChoice();
    }
}