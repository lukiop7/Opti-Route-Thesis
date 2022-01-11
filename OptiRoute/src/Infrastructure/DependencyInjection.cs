using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Infrastructure.FileReaders.Services;
using OptiRoute.Infrastructure.Files;
using OptiRoute.Infrastructure.Files.FileReaders.BenchmarkTemplate;
using OptiRoute.Infrastructure.Files.FileReaders.BenchmarkTemplates;
using OptiRoute.Infrastructure.Files.FileReaders.Services;
using OptiRoute.Infrastructure.HostedService;
using OptiRoute.Infrastructure.Persistence;
using OptiRoute.Infrastructure.Services;

namespace OptiRoute.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("OptiRouteDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IBenchmarkInstanceFileReader, BenchmarkInstanceFileReader>();
            services.AddTransient<IBenchmarkBestFileReader, BenchmarkBestFileReader>();
            services.AddTransient<IFileProviderService, SolomonInstancesFileProviderService>();

            services.AddSingleton<BenchmarkBestTemplate>();
            services.AddSingleton<BenchmarkInstanceTemplate>();

            services.AddHostedService<SolomonBenchmarkHostedService>();

            return services;
        }
    }
}