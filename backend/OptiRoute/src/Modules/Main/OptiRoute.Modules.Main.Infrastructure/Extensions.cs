using Microsoft.Extensions.DependencyInjection;
using OptiRoute.Modules.Main.Domain.Interfaces;
using OptiRoute.Modules.Main.Infrastructure.Repositories;

namespace OptiRoute.Modules.Main.Infrastructure
{
   public static class Extensions
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITestRepository, TestRepository>();
            return services;
        }
    }
}
