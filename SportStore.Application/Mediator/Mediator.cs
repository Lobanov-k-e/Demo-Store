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
    public class Mediator
    {
        private static readonly Dictionary<Type, Type> Handlers = new Dictionary<Type, Type>(); //concurrent?
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

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
            catch (KeyNotFoundException e)
            {
                throw new HandlerNotFoundException($"Handler for type {request.GetType()} not found", e);
            }

            var handler = Activator.CreateInstance(handlerType, _context, _mapper);
            return handler;
        }

        public static void Register<TRequest, THandler>()
        {
            Handlers.Add(typeof(TRequest), typeof(THandler));
        }

        public static void RegisterFromAssembly(Assembly assembly)
        {            
            var requestTypes = GetRequestTypesFromAssembly(assembly);
            foreach (var requestType in requestTypes)
            {
                Type handlerType = GetHandler(assembly, requestType);
                Handlers.Add(requestType, handlerType);                
            }
        }

        private static Type GetHandler(Assembly assembly, Type requestType)
        {
            return assembly.GetTypes().Where(t =>
                    t.GetInterfaces().Any(i => i.IsGenericType
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
