using Microsoft.Extensions.DependencyInjection;
using OptiRoute.Modules.Main.Application.Services;
using OptiRoute.Modules.Main.Domain;
using OptiRoute.Modules.Main.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Modules.Main.Application
{
    public static class Extensions
    {
        /// <summary>
        /// Adds the OptiRoute.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddMain(this IServiceCollection services)
        {
            services.AddScoped<ITestService, TestService>();
            services
                .AddDomain()
                .AddInfrastructure();

            return services;
        }
    }
}
