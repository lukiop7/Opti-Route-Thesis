using Microsoft.EntityFrameworkCore;
using OptiRoute.Shared.Abstractions.Repositories;
using OptiRoute.Shared.Data.Models;

namespace OptiRoute.Shared.Infrastructure.Repositories
{
    /// <summary>

    public abstract class BaseReadOnlyRepository<TEntity, TContext> : BaseRepository<TEntity, TContext>, IReadOnlyRepository<TEntity, TContext>
               where TEntity : class
        where TContext : DbContext
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="BaseReadOnlyRepository{TEntity, TContext}"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        protected BaseReadOnlyRepository(TContext context) : base(context)
        {
        }
    }
}