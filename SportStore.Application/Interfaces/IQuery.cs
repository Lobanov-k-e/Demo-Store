using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Application.Interfaces
{
    public interface IQuery<T>
    {
        Task<T> Execute();
    }
}
