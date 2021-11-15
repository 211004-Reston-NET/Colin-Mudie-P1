using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using WebUI.Data;

[assembly: HostingStartup(typeof(WebUI.Areas.Identity.IdentityHostingStartup))]
namespace WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TestDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TestDBContextConnection")));

                services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<TestDBContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}