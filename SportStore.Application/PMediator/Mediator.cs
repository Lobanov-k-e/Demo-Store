using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SportStore.Application
{
    // I can make Handlers non static
    // registration will become time-consuming, i can register requested types on the fly
    // and saving them in Handlers for later use

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        protected static Dictionary<Type, Type> Handlers { get; set; } = new Dictionary<Type, Type>();

        public Mediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
             
        public async Task<TResult> Handle<TResult>(IRequest<TResult> request)
        {
            dynamic handler = GetHandler(request);
            dynamic requestImpl = request;
            return await handler.Handle(requestImpl);
        }
        protected dynamic GetHandler<TResult>(IRequest<TResult> request)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            Type handlerType;
            try
            {
                handlerType = Handlers[request.GetType()];
            }
            catch (KeyNotFoundException)
            {
                throw new HandlerNotFoundException($"Handler for type {request.GetType()} not found");
            }

            var constructor = handlerType.GetConstructors().Single();
            List<object> arguments = ResolveConstructorParameters(constructor);

            object handler = CreateHandler(constructor, arguments);

            return handler;

            static object CreateHandler(ConstructorInfo constructor, List<object> arguments)
            {
                return constructor.Invoke(arguments.ToArray());
            }
        }

        private List<object> ResolveConstructorParameters(ConstructorInfo constructor)
        {
            var arguments = new List<object>(constructor.GetParameters().Length);

            arguments.AddRange(
                from ctorParam in constructor.GetParameters()
                select _serviceProvider.GetService(ctorParam.ParameterType));

            return arguments;
        }

        public static void Register<TRequest, THandler>()
        {
            Register(typeof(TRequest), typeof(THandler));
        }
        private static void Register(Type requestType, Type handlerType)
        {
            Handlers.Add(requestType, handlerType);
        }
        public static void RegisterFromAssembly(Assembly assembly)
        {
            var requestTypes = GetRequestTypesFromAssembly(assembly);
            foreach (var requestType in requestTypes)
            {
                Type handlerType = GetHandlerType(assembly, requestType);
                Register(requestType, handlerType);
            }
        }

        private static Type GetHandlerType(Assembly assembly, Type requestType)
        {
            return assembly.GetTypes().Where(t =>
                    t.GetInterfaces().Any(i => 
                           i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)
                        && i.GetGenericArguments().Contains(requestType)
                        )).Single();
        }

        private static IEnumerable<Type> GetRequestTypesFromAssembly(Assembly assembly)
        {
            return assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => 
                i.IsGenericType && 
                i.GetGenericTypeDefinition() == typeof(IRequest<>)));
        }
    }
}
