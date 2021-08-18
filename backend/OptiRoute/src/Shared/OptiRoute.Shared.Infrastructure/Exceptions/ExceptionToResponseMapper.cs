using System;
using System.Collections.Concurrent;
using System.Net;

//using Humanizer;
using OptiRoute.Shared.Abstractions.Exceptions;

namespace OptiRoute.Shared.Infrastructure.Exceptions
{
    /// <summary>
    /// Interfejs reprezentujący mapowanie błędów napotkanych w trakcie wykonywania zapytania.
    /// Implementuje<see cref="OptiRoute.Shared.Abstractions.Exceptions.IExceptionToResponseMapper" />
    /// </summary>
    /// <seealso cref="OptiRoute.Shared.Abstractions.Exceptions.IExceptionToResponseMapper" />
    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new ConcurrentDictionary<Type, string>();

        /// <summary>
        /// Metoda agregująca błędy i zwracająca listę błędów.
        /// </summary>
        /// <param name="exception">Napotkany błąd</param>
        /// <returns>ExceptionResponse.</returns>
        public ExceptionResponse Map(Exception exception)
            => exception switch
            {
                Exception ex => new ExceptionResponse(
                    new ErrorsResponse(new Error("error", ex.Message)), HttpStatusCode.InternalServerError)
            };

        /// <summary>
        /// Metoda pobierająca kod wyjątku.
        /// </summary>
        /// <param name="exception">Wyjątek</param>
        /// <returns>System.String.</returns>
        private static string GetErrorCode(object exception)
        {
            var type = exception.GetType();
            return Codes.GetOrAdd(type, (typeArg) => typeArg.Name.Replace("_exception", string.Empty));
        }
    }
}