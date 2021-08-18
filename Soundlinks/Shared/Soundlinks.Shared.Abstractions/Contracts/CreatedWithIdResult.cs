namespace Soundlinks.Shared.Abstractions.Contracts
{
    /// <summary>
    /// Klasa reprezentująca odpowiedź o poprawnym utworzeniu obiektu w bazie zwracając jego ID.
    /// Implementuje <see cref="Soundlinks.Shared.Abstractions.Contracts.OperationResult" />
    /// </summary>
    /// <seealso cref="Soundlinks.Shared.Abstractions.Contracts.OperationResult" />
    public class CreatedWithIdResult : OperationResult
    {
        public long Id { get; set; }

        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <see cref="CreatedWithIdResult"/>.
        /// </summary>
        /// <param name="id">Zwracane ID obiektu</param>
        public CreatedWithIdResult(long id)
        {
            Id = id;
        }
    }
}
