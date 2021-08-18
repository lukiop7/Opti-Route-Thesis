using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OptiRoute.Shared.Abstractions.Repositories;

namespace OptiRoute.Shared.Infrastructure.Repositories
{
    /// <summary>
    /// Klasa implementująca podstawowe metody repozytorium.
    /// Implementuje <see cref="OptiRoute.Shared.Infrastructure.Repositories.BaseRepository{TEntity, TContext}" />
    /// Implementuje <see cref="OptiRoute.Shared.Abstractions.Repositories.ICrudRepository{TEntity}" />
    /// </summary>
    /// <typeparam name="TEntity">Typ obiektów repozytorium.</typeparam>
    /// <typeparam name="TContext">Typ kontekstu bazy danych</typeparam>
    /// <seealso cref="OptiRoute.Shared.Infrastructure.Repositories.BaseRepository{TEntity, TContext}" />
    /// <seealso cref="OptiRoute.Shared.Abstractions.Repositories.ICrudRepository{TEntity}" />
    public abstract class BaseCrudRepository<TEntity, TContext> : BaseRepository<TEntity, TContext>, ICrudRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="BaseCrudRepository{TEntity, TContext}"/>.
        /// </summary>
        /// <param name="context">Kontekst bazy danych</param>
        protected BaseCrudRepository(TContext context) : base(context)
        {
        }

        /// <summary>
        /// Asynchroniczna metoda dodająca obiekt do bazy.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        /// <returns>Task.</returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        /// <summary>
        /// Metoda uaktualniająca obiekt w bazie danych.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        public virtual void Update(TEntity entity)
        {
            AttachDetachedEntry(entity);
            SetModified(entity);
        }

        /// <summary>
        /// Metoda usuwająca obiekt z bazy danych.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        public virtual void Delete(TEntity entity)
        {
            AttachDetachedEntry(entity);
            SetDeleted(entity);
        }

        /// <summary>
        /// Asynchroniczna metoda usuwająca obiekt z bazy danych na podstawie podanego ID.
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Task.</returns>
        public virtual async Task DeleteByIdAsync(long id)
        {
            var entity = await GetAsync(id);

            if (entity != null)
            {
                Delete(entity);
            }
        }

        /// <summary>
        /// Zmienia status podpięcia obiektu.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        protected void AttachDetachedEntry(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
        }

        /// <summary>
        /// Ustawia stan obiektu na zmodyfikowany.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        protected void SetModified(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Ustawia stan obiektu na usunięty.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        protected void SetDeleted(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Ustawia stan obiektu na niezmieniony.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        protected void SetUnchanged(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Unchanged;
        }
    }
}