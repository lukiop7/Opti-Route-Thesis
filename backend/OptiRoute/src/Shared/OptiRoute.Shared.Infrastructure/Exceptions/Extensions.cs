using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OptiRoute.Shared.Abstractions.Exceptions;

namespace OptiRoute.Shared.Infrastructure.Exceptions
{
    public static class Extensions
    {
        public static IServiceCollection AddErrorHandling(this IServiceCollection services)
            => services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();

        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
            => app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}