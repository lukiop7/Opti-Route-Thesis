using System.Threading.Tasks;

namespace Soundlinks.Shared.Abstractions.Repositories
{
    /// <summary>
    /// Interfejs reprezentujący dynamiczny słownik repozytorium.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDynamicDictionaryRepository<T>
    {
        /// <summary>
        /// Metoda asynchroniczna dodająca lub pobierająca obiekt po jego nazwie. Może zwracać null.
        /// </summary>
        /// <param name="name">Nazwa obiektu.</param>
        /// <returns>Task&lt;System.Nullable&lt;System.Int64&gt;&gt;.</returns>
        Task<long?> GetOrAddByNameNullable(string name);

        /// <summary>
        /// Metoda asynchroniczna dodająca lub pobierająca obiekt po jego nazwie.
        /// </summary>
        /// <param name="name">Nazwa obiektu</param>
        /// <returns>Task&lt;System.Int64&gt;.</returns>
        Task<long> GetOrAddByName(string name);
    }
}