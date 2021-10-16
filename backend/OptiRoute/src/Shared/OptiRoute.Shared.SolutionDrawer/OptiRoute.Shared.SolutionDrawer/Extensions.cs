using Microsoft.Extensions.DependencyInjection;

namespace OptiRoute.Shared.SolutionDrawer
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSolutionDrawer(this IServiceCollection services)
        {
            services.AddScoped<ISolutionDrawer, SolutionDrawer>();
            return services;
        }
    }
}
