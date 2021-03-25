using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SportStore.Infrastructure;
using SportStore.Infrastructure.Authorization;
using SportStore.Infrastructure.Persistence;

namespace SportStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var appContext = services.GetRequiredService<ApplicationContext>();

                    if (appContext.Database.IsSqlServer())
                    {
                        appContext.Database.Migrate();
                    }

                    var identityContext = services.GetRequiredService<IdentityContext>();

                    if (identityContext.Database.IsSqlServer())
                    {
                        identityContext.Database.Migrate();
                    }

                    //await SeedData.Initialize(appContext);

                    //var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                    //await SeedIdentity.Initialize(userManager);
                }
                catch (Exception ex)
                {
                    //log db migrat or db create exception here
                    throw;
                }

                await host.RunAsync();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
