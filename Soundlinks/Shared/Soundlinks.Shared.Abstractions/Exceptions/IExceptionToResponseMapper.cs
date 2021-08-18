using System;

namespace Soundlinks.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Interfejs reprezentujący mapowanie błędów napotkanych w trakcie wykonywania zapytania.
    /// </summary>
    public interface IExceptionToResponseMapper
    {
        /// <summary>
        /// Metoda agregująca błędy i zwracająca listę błędów.
        /// </summary>
        /// <param name="exception">Napotkany błąd</param>
        /// <returns>ExceptionResponse.</returns>
        ExceptionResponse Map(Exception exception);
    }
}