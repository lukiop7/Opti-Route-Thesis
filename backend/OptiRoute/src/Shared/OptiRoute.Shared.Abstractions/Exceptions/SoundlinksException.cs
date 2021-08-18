using System;

namespace OptiRoute.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Klasa reprezentująca własny, implementowany wyjątek obsługiwany przez aplikację.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class OptiRouteException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="OptiRouteException"/>.
        /// </summary>
        /// <param name="message">Wiadomość opisująca napotkany wyjątek.</param>
        protected OptiRouteException(string message) : base(message)
        {
        }
    }
}