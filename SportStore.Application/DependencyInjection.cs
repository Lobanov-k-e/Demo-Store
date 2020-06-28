using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Categories.Commands;
using SportStore.Application.Interfaces;
using SportStore.Application.Orders;
using SportStore.Application.Products;
using System;
using System.Linq;
using System.Reflection;

namespace SportStore.Application
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection AddApplication(this IServiceCollection services)       
        {            
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMediator, Mediator>();
            //services.AddTransient<IValidator<OrderVm>, OrderVmValidation>();
            //services.AddTransient<IValidator<EditCategory>, EditCategoryValidator>();
            //services.AddTransient<IValidator<AddCategoryCommand>, AddCategoryValidator>();
            RegisterHandlers();
            RegisterValidators(services);
            return services;
        }

        private static void RegisterHandlers()
        {
            Mediator.RegisterFromAssembly(typeof(Mediator).Assembly);
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var validators = assembly
                .GetTypes()
                .Where(t => t.BaseType != null
                            && t.BaseType.IsGenericType
                            && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));                     
             
            foreach (var validator in validators)
            {
                var validatorBase = validator.BaseType.GetInterfaces()
                    .Where(i => 
                    i.IsGenericType 
                    && i.GetGenericTypeDefinition() == typeof(IValidator<>) 
                    ).Single();
                
                services.AddTransient(validatorBase, validator);
            }
        }

    }
}
