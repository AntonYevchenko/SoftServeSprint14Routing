using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsWithRouting.Services;

namespace ProductsWithRouting
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
            services.AddControllersWithViews();
            services.AddSingleton<Data>();
            services.AddSingleton<FilterProductsService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region list of products page available
                endpoints.MapControllerRoute(
                    name: "products_index",
                    pattern: "products/index",
                    defaults: new { controller = "Products", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "items_index",
                    pattern: "items/index",
                    defaults: new { controller = "Products", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "products_root",
                    pattern: "products",
                    defaults: new { controller = "Products", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "items_root",
                    pattern: "items",
                    defaults: new { controller = "Products", action = "Index" });
                #endregion

                endpoints.MapControllerRoute(
                    name: "new_product",
                    pattern: "products/new",
                    defaults: new { controller = "Products", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "create_product",
                    pattern: "products/create",
                    defaults: new { controller = "Products", action = "Create" });

                endpoints.MapControllerRoute(
                    name: "view_product",
                    pattern: "products/{id}",
                    defaults: new { controller = "Products", action = "View" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
