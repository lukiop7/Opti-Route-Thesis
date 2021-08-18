using Microsoft.Extensions.DependencyInjection;

namespace Soundlinks.Modules.Soundlinks.Infrastructure
{
    /// <summary>
    /// The extensions for Soundlinks module.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // TODO: register repositories

            return services;
        }
    }
}
