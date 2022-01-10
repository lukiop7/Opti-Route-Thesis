using Microsoft.EntityFrameworkCore;
using OptiRoute.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<TodoList> TodoLists { get; set; }
        DbSet<BenchmarkResult> BenchmarkResults { get; set; }
        DbSet<BenchmarkInstance> BenchmarkInstances { get; set; }
        DbSet<Route> Routes { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Depot> Depots { get; set; }
        DbSet<Solution> Solutions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}