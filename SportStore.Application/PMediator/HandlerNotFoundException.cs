using System;
using System.Collections.Generic;
using System.Text;

namespace SportStore.Application
{
    public class HandlerNotFoundException : Exception
    {
        public HandlerNotFoundException(string message) 
            : base(message)
        {
        }
    }
}
