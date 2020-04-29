using Microsoft.EntityFrameworkCore;
using SportStore.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SportStore.Application.Interfaces
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
