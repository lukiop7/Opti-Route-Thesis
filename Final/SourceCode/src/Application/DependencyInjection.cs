using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OptiRoute.Application.Common.Behaviours;
using System.Reflection;

namespace OptiRoute.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient<IMethod, PFIHInitial>();
            services.AddTransient<IImprovement, LocalSearchLambda>();
            services.AddTransient<ISolver, VRPTWSolver>();

            return services;
        }
    }
}