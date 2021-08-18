using System;

namespace Soundlinks.Shared.Abstractions.Exceptions
{
    /// <summary>
    /// Klasa reprezentująca własny, implementowany wyjątek obsługiwany przez aplikację.
    /// Implements the <see cref="System.Exception" />
    /// </summary>
    /// <seealso cref="System.Exception" />
    public abstract class SoundlinksException : Exception
    {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="SoundlinksException"/>.
        /// </summary>
        /// <param name="message">Wiadomość opisująca napotkany wyjątek.</param>
        protected SoundlinksException(string message) : base(message)
        {
        }
    }
}