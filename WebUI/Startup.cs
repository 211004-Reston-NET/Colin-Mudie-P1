using Business_Logic;
using Data_Access_Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Data;

namespace WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddDbContext<MMDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MMDB")));
            services.AddDbContext<TestDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TestDBContextConnection")));
            services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = false)
                        .AddEntityFrameworkStores<TestDBContext>()
                        .AddDefaultTokenProviders();
           // services.AddIdentity<Customer>(options => options.Cookies.ApplicationCookie.LoginPath = new PathString("/Admin/Account/Login"));
            services.AddScoped<IStoreFrontBL, StoreFrontBL>();
            services.AddScoped<ICustomerBL, CustomerBL>();
            services.AddScoped<ILineItemsBL, LineItemsBL>();
            services.AddScoped<IOrderBL, OrderBL>();
            services.AddScoped<IProductBL, ProductBL>();
            services.AddScoped<IRepository, RepositoryCloud>();
            services
                .AddAuthentication()
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Login";
                options.LogoutPath = $"/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

        

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
