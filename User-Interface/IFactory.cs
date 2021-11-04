namespace User_Interface
{
    public interface IFactory
    {
        /// <summary>
        /// Will create a new class that extends IMenu based on the inputted enum MenuType.
        /// </summary>
        /// <param name="p_menu"> This will determine what menu will be created. </param>
        /// <returns> returns a new class that implements IMenu. </returns>
        IMenu GetMenu(MenuType p_menu);
    }
}