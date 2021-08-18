using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Soundlinks.Shared.Abstractions.Exceptions;

namespace Soundlinks.Shared.Infrastructure.Exceptions
{
    /// <summary>
    /// Klasa implementująca middleware obsługi błędów i wyjątków.
    /// </summary>
    internal class ErrorHandlerMiddleware
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        /// <summary>
        /// Inicjalizuje nowa instancję klasy <see cref="ErrorHandlerMiddleware"/>.
        /// </summary>
        /// <param name="next">Dalszy middleware zapytania</param>
        /// <param name="serviceProvider">Service provider</param>
        /// <param name="logger">Logger</param>
        public ErrorHandlerMiddleware(RequestDelegate next, IServiceProvider serviceProvider, ILogger<ErrorHandlerMiddleware> logger)
        {
            this.serviceProvider = serviceProvider;
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Asynchroniczna metoda obsługująca wywołanie akcji zapytania i oczekująca na ewentualne wyjątki.
        /// </summary>
        /// <param name="context">Kontekst http zapytania</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var exceptionToLog = exception is AggregateException ? exception.GetBaseException() : exception;
                logger.LogError(exceptionToLog, exceptionToLog.Message);
                await HandleErrorAsync(context, exceptionToLog);
            }
        }

        /// <summary>
        /// Asynchroniczna metoda obsługująca napotkany wyjątek i zwracająca odpowiedź z kodem błędu.
        /// </summary>
        /// <param name="context">Kontekst http zapytania</param>
        /// <param name="exception">Wyjątek</param>
        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            using var scope = serviceProvider.CreateScope();
            var responseMapper = scope.ServiceProvider.GetService<IExceptionToResponseMapper>();
            var exceptionResponse = responseMapper.Map(exception);
            var message = JsonSerializer.Serialize(exceptionResponse.Response);
            context.Response.StatusCode = (int)exceptionResponse.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(message);
        }
    }
}