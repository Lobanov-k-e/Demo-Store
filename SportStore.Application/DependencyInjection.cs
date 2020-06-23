using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Categories.Commands;
using SportStore.Application.Interfaces;
using SportStore.Application.Orders;
using SportStore.Application.Products;

namespace SportStore.Application
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddApplication(this IServiceCollection services)       
        {            
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMediator, Mediator>();
            services.AddTransient<IValidator<OrderVm>, OrderVmValidation>();
            services.AddTransient<IValidator<EditCategory>, EditCategoryValidator>();
            services.AddTransient<IValidator<AddCategoryCommand>, AddCategoryValidator>();
            RegisterHandlers();
            return services;
        }

        private static void RegisterHandlers()
        {
            Mediator.RegisterFromAssembly(typeof(Mediator).Assembly);
        }

    }
}
