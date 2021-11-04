using System.IO;
using Business_Logic;
using Data_Access_Logic;
using Data_Access_Logic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace User_Interface
{
    public class MenuFactory : IFactory
    {
        public IMenu GetMenu(MenuType p_menu)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();
            DbContextOptions<MMDBContext> options = new DbContextOptionsBuilder<MMDBContext>()
                .UseSqlServer(configuration.GetConnectionString("MMDB"))
                .Options;

            switch (p_menu)
            {
                case MenuType.StartMenu:
                    return new StartMenu();

                case MenuType.MainMenu:
                    return new MainMenu();

                case MenuType.AddCustomer:
                    return new AddCustomer(new CustomerBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.CustomerMenu:
                    return new CustomerMenu(new CustomerBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.SearchForCustomer:
                    return new SearchCustomers(new CustomerBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.ShowStoreFronts:
                    return new ShowStoreFronts(new StoreFrontBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.ShowLineItems:
                    return new ShowLineItems(new LineItemsBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.SearchByCategory:
                    return new SearchByCategory(new ProductBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.PlaceOrder:
                    return new PlaceOrder(new CustomerBL(new RepositoryCloud(new MMDBContext(options))), new LineItemsBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.ViewOrderHistory:
                    return new ShowOrders(new OrderBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.StockRefresh:
                    return new StockRefresh(new LineItemsBL(new RepositoryCloud(new MMDBContext(options))));

                case MenuType.ShowCustomers:
                    return new ShowCustomers(new CustomerBL(new RepositoryCloud(new MMDBContext(options))));

                default:
                    return null;
            }
        }
    }
}