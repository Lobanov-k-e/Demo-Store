using SportStore.Application.Interfaces;
using SportStore.Application;
using SportStore.Application.Products;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace SportStore.Application
{
    // I can make handlers non static
    // registration will become time-consuming, i can register requested types on the fly
    // and saving them in handlers for later use

    // handler parameters must be injected to make madiator reusable
    // or make mediator abstract and and overload MakeHandler
    
    public class Mediator : IMediator
    {
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        protected static Dictionary<Type, Type> Handlers { get; set; } = new Dictionary<Type, Type>();

        public Mediator(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

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

            var handler = Activator.CreateInstance(handlerType, _context, _mapper);
            return handler;
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
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)));
        }
    }
}
