using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OptiRoute.Shared.Abstractions.Contracts;
using OptiRoute.Shared.Abstractions.Repositories;

namespace OptiRoute.Shared.Infrastructure.Repositories
{
    /// <summary>
    /// Klasa implementująca podstawowe metody repozytorium.
    /// Implementuje <see cref="OptiRoute.Shared.Abstractions.Repositories.IRepository{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity">Typ obiektu/entity.</typeparam>
    /// <typeparam name="TContext">Typ kontekstu bazy danych.</typeparam>
    /// <seealso cref="OptiRoute.Shared.Abstractions.Repositories.IRepository{TEntity}" />
    public abstract class BaseRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext Context { get; }

        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="BaseRepository{TEntity, TContext}"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        protected BaseRepository(TContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        /// <summary>
        /// Asynchroniczna metoda sprawdzająca czy dany obiekt istnieje w repozytorium.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca obiekt z repozytorium na podstawie kryteriów wyszukiwania.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <param name="includeReferences">Referencja do obiektu</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeReferences = null)
        {
            return includeReferences == null
                ? DbSet.AsNoTracking().FirstOrDefaultAsync(predicate)
                : includeReferences(DbSet).AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca obiekt z repozytorium na podstawie ID.
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Task&lt;T&gt;.</returns>
        public virtual async Task<TEntity> GetAsync(long id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca wszystkie obiekty z repozytorium.
        /// </summary>
        /// <returns>Task&lt;ICollection&lt;T&gt;&gt;.</returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await IncludeListReferences(DbSet.AsNoTracking()).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca wszystkie obiekty z repozytorium spełniające kryteria wyszukiwania.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <returns>Task&lt;ICollection&lt;T&gt;&gt;.</returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await IncludeListReferences(DbSet.Where(predicate)).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca dane z paginacji danych (konkretną stronę)
        /// </summary>
        /// <param name="page">Numer strony</param>
        /// <param name="pageSize">Liczba obiektów na stronie</param>
        /// <returns>Task&lt;PagedResult&lt;TEntity&gt;&gt;.</returns>
        public virtual Task<PagedResult<TEntity>> GetPageAsync(int page, int pageSize)
        {
            return GetPageAsync(entity => true, page, pageSize);
        }

        /// <summary>
        /// Asynchroniczna metoda pobierająca dane z paginacji danych (konkretną stronę) na podstawie kryteriów wyszukiwania. 
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <param name="page">Numer strony</param>
        /// <param name="pageSize">Liczba obiektów na stronie</param>
        /// <returns>Task&lt;PagedResult&lt;TEntity&gt;&gt;.</returns>
        public virtual async Task<PagedResult<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize)
        {
            var entities = EntitiesSelector(DbSet).Where(predicate);
            var totalCount = await entities.CountAsync();
            var results = await IncludeListReferences(entities
                .Skip(page - 1 * pageSize)
                .Take(pageSize))
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return new PagedResult<TEntity>
            {
                Results = results,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        /// <summary>
        /// Metoda dołączająca listę referencji.
        /// </summary>
        /// <param name="entities">Obiekt listy</param>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        protected virtual IQueryable<TEntity> IncludeListReferences(IQueryable<TEntity> entities)
        {
            return entities;
        }

        /// <summary>
        /// Metoda pobierająca listę obiektów.
        /// </summary>
        /// <param name="entities">Lista obiektów</param>
        /// <returns>IQueryable&lt;TEntity&gt;.</returns>
        protected virtual IQueryable<TEntity> EntitiesSelector(IQueryable<TEntity> entities)
        {
            return entities;
        }
    }
}