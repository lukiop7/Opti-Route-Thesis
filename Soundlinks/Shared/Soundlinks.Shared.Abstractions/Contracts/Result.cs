namespace Soundlinks.Shared.Abstractions.Contracts
{
    /// <summary>
    /// Klasa reprezentująca zwracaną odpowiedź z zapytania
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T>
    {
        public T Payload { get; set; }

        public string Error { get; set; }
    }
}
