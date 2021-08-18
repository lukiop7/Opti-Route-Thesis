namespace Soundlinks.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Klasa reprezentująca błąd napotkany w trakcie działania aplikacji.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <see cref="Error"/>.
        /// </summary>
        /// <param name="code">Kod błędu</param>
        /// <param name="message">Wiadomość</param>
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }

        public string Message { get; set; }
    }
}