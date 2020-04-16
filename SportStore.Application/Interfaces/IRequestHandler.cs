using SportStore.Application.Interfaces;
using System.Threading.Tasks;

namespace SportStore.Application.Products.Queries
{
    public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> Handle(TRequest request);
    }
}