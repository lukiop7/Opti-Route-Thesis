namespace Soundlinks.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Klasa reprezentująca odpowiedź zapytania, jeśli pojawiły się błędy w trakcie jego wykonania. Zwraca listę błędów.
    /// </summary>
    public class ErrorsResponse
    {
        public ErrorsResponse(params Error[] errors)
        {
            Errors = errors;
        }

        public Error[] Errors { get; set; }
    }
}