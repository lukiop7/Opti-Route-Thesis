using System.Threading.Tasks;

namespace Soundlinks.Shared.Abstractions.Repositories
{
    /// <summary>
    /// Interfejs reprezentujący repozytorium zawierające podstawowe operację na bazie danych.
    /// Implementuje <see cref="Soundlinks.Shared.Abstractions.Repositories.IRepository{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Soundlinks.Shared.Abstractions.Repositories.IRepository{T}" />
    public interface ICrudRepository<T> : IRepository<T>
    {
        /// <summary>
        /// Asynchroniczna metoda dodająca obiekt do bazy.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        /// <returns>Task.</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Metoda uaktualniająca obiekt w bazie danych.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        void Update(T entity);

        /// <summary>
        /// Metoda usuwająca obiekt z bazy danych.
        /// </summary>
        /// <param name="entity">Obiekt</param>
        void Delete(T entity);

        /// <summary>
        /// Asynchroniczna metoda usuwająca obiekt z bazy danych na podstawie podanego ID.
        /// </summary>
        /// <param name="id">Identyfikator obiektu</param>
        /// <returns>Task.</returns>
        Task DeleteByIdAsync(long id);
    }
}