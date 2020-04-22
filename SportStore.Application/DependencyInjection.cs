using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Interfaces;
using SportStore.Application.Products;

namespace SportStore.Application
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddApplication(this IServiceCollection services)       
        {            
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMediator, Mediator>();
            RegisterHandlers();
            return services;
        }

        private static void RegisterHandlers()
        {
            Mediator.RegisterFromAssembly(typeof(Mediator).Assembly);
        }

    }
}
