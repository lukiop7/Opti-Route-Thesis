using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Domain.Common;
using OptiRoute.Domain.Entities;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public DbSet<BenchmarkResult> BenchmarkResults { get; set; }
        public DbSet<BenchmarkInstance> BenchmarkInstances { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<Solution> Solutions { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}