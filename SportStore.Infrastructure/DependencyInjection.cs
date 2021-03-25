using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Interfaces;
using SportStore.Infrastructure.Authorization;
using SportStore.Infrastructure.Persistence;

namespace SportStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            //add store data context
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());

            //add identity data context
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("IdentityConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IIdentityContext>(provider => provider.GetService<IdentityContext>());            
            return services;
        }
    }
}
