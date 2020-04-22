using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportStore.Application;
using SportStore.Infrastructure;
using System;

namespace SportStore
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration 
                ?? throw new System.ArgumentNullException(paramName: nameof(configuration),
                message: "Configoration should not be null");
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = services.AddControllersWithViews();           
            services.AddApplication();
            services.AddInfrastructure(_configuration);
#if DEBUG           
                builder.AddRazorRuntimeCompilation();            
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "{currentCategory}/Page{pageNumber:int}",
                    defaults: new { Controller = "Product", Action = "ProductList" });
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "/Page{pageNumber:int}",
                    defaults: new { Controller = "Product", Action = "ProductList", pageNumber = 1});
                endpoints.MapControllerRoute(
                   name: null,
                   pattern: "{currentCategory}",
                   defaults: new { Controller = "Product", Action = "ProductList", pageNumber = 1 });
                endpoints.MapControllerRoute(
                   name: null,
                   pattern: "",
                   defaults: new { Controller = "Product", Action = "ProductList", pageNumber = 1 });
                endpoints.MapControllerRoute("default", "{controller=Product}/{action=ProductList}/{id?}");                          
            });
        }
    }
}
