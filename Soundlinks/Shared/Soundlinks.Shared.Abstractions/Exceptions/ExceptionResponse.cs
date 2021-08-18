using System.Net;

namespace Soundlinks.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Klasa reprezentująca odpowiedź na zapytanie, jeśli pojawiły się wyjątki w trakcie wykonywania zapytania.
    /// </summary>
    public class ExceptionResponse
    {
        /// <summary>
        /// Inicjalizuje nowy obiekt klasy <see cref="ExceptionResponse"/>.
        /// </summary>
        /// <param name="response">Obiekt odpowiedzi</param>
        /// <param name="statusCode">Status błędu</param>
        public ExceptionResponse(object response, HttpStatusCode statusCode)
        {
            Response = response;
            StatusCode = statusCode;
        }

        public object Response { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}