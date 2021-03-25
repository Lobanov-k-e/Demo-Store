using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SportStore.Infrastructure.Authorization
{
    public interface IUserService
    {
        Task<bool> CreateAsync(string userName, string password);
        Task<IdentityUser> GetByUsername(string userName);
    }
}