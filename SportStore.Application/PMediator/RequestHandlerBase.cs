using SportStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application
{
    public abstract class RequestHandlerBase
    {
        public RequestHandlerBase(IApplicationContext context, IMapper mapper)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected IApplicationContext Context { get; }
        protected IMapper Mapper { get; }
    }
}
