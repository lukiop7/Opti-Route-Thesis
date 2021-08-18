using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OptiRoute.WebApi
{
    /// <summary>
    /// The API extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var frontendClientURL = configuration.GetSection("FrontendClientURL").Value;

            var assemblies = new List<Assembly>
            {
                AppDomain.CurrentDomain.Load("OptiRoute.Modules.Main.Application"),
            };

            //CORS POLICY
            services.AddCors(options =>
            {
                options.AddPolicy("FrontendClient", builder =>
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(frontendClientURL)
                );
            });

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(assemblies);

            services.AddMediatR(assemblies.ToArray());

            services.AddSwaggerDocumentation();

            return services;
        }

        /// <summary>
        /// Uses the infrastructure.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors("FrontendClient");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }

        /// <summary>
        /// Adds the swagger documentation.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OptiRoute.WebApi",
                    Version = "v1"
                });

                c.CustomSchemaIds(ss => ss.FullName);
            });

            return services;
        }

        /// <summary>
        /// Uses the swagger documentation.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptiRoute.WebApi"));

            return app;
        }
    }
}
