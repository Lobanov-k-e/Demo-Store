using SportStore.Application.Interfaces;
using System.Threading.Tasks;

namespace SportStore.Application
{
    public interface IMediator
    {
        Task<TResult> Handle<TResult>(IRequest<TResult> request);
    }
}