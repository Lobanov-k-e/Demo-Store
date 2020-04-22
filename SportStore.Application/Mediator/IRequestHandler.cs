
using System.Threading.Tasks;

namespace SportStore.Application.Interfaces
{
    public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest request);
    }
}