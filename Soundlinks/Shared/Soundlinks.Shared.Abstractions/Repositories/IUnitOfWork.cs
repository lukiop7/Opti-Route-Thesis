using System.Threading.Tasks;

namespace Soundlinks.Shared.Abstractions.Repositories
{
    /// <summary>
    /// Interfejs reprezentujący wykonanie akcji na kontekście bazy danych
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Asynchroniczna metoda do zapisywania zmian na kontekście bazy danych
        /// </summary>
        /// <returns>Task.</returns>
        Task SaveChangesAsync();
    }
}