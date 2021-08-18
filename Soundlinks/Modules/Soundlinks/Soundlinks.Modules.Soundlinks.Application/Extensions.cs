using Microsoft.Extensions.DependencyInjection;
using Soundlinks.Modules.Soundlinks.Infrastructure;

namespace Soundlinks.Modules.Soundlinks.Application
{
    /// <summary>
    /// The extensions for Soundlinks module.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the Soundlinks.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSoundlinks(this IServiceCollection services)
        {
            services.AddInfrastructure();

            return services;
        }
    }
}
