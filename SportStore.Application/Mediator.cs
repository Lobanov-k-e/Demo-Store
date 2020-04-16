using SportStore.Application.Interfaces;
using SportStore.Application.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SportStore.Application.Products.Queries;

namespace SportStore.Application
{
    public class RequestHandler
    {
        private static Dictionary<Type, Func<object>> Handlers = new Dictionary<Type, Func<object>>();
        private readonly IApplicationContext _context;
        private readonly IMapper _mapper;

        public RequestHandler(IApplicationContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TResult> Handle<TResult>(IRequest<TResult> request)
        {           
            IRequestHandler<IRequest<TResult>, TResult> handler = GetHandler(request);
            return await handler.Handle(request);        
        }

        private static IRequestHandler<IRequest<TResult>, TResult> GetHandler<TResult>(IRequest<TResult> request)
        {
           //catch keynotfound
            var handler = Handlers[request.GetType()].Invoke() as IRequestHandler<IRequest<TResult>, TResult>;
            return handler;                    
        }

        public static void Register<TRequest, THandler>() where THandler : new() 
        {
            Handlers.Add(typeof(TRequest), () => new THandler());
        }
    }
}
