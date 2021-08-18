using System.Collections.Generic;
using System.Linq;

namespace OptiRoute.Shared.Abstractions.Contracts
{
    /// <summary>
    /// Klasa reprezentująca odpowiedź na zapytanie http.
    /// </summary>
    public class OperationResult
    {
        public bool Success => !ErrorMessages.Any();

        public ICollection<string> ErrorMessages { get; set; } = new List<string>();
    }
}
