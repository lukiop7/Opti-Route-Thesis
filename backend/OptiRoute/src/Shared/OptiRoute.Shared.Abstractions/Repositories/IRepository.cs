using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OptiRoute.Shared.Abstractions.Repositories
{
    /// <summary>
    /// Interfejs reprezentujący Repozytorium.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Asynchroniczna metoda sprawdzająca czy dany obiekt istnieje w repozytorium.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchroniczna metoda pobierająca wszystkie obiekty z repozytorium.
        /// </summary>
        /// <returns>Task&lt;ICollection&lt;T&gt;&gt;.</returns>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>
        /// Asynchroniczna metoda pobierająca wszystkie obiekty z repozytorium spełniające kryteria wyszukiwania.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <returns>Task&lt;ICollection&lt;T&gt;&gt;.</returns>
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchroniczna metoda pobierająca obiekt z repozytorium o podanym ID.
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetAsync(long id);

        /// <summary>
        /// Asynchroniczna metoda pobierająca obiekt z repozytorium na podstawie kryteriów wyszukiwania.
        /// </summary>
        /// <param name="predicate">Wyrażenie query</param>
        /// <param name="includeReferences">Referencja do obiektu</param>
        /// <returns>Task&lt;T&gt;.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> includeReferences = null);
    }
}