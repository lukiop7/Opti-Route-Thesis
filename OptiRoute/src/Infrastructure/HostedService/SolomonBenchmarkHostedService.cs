using AlgorithmCoreVRPTW.Solver.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptiRoute.Application.Common.Enums;
using OptiRoute.Application.Common.Exceptions;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Domain.Entities;
using OptiRoute.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Infrastructure.HostedService
{
    public class SolomonBenchmarkHostedService : IHostedService
    {
        private IServiceProvider _serviceProvider;

        public SolomonBenchmarkHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProviderService>();
                var benchmarkInstanceFileReader = scope.ServiceProvider.GetRequiredService<IBenchmarkInstanceFileReader>();
                var benchmarkBestFileReader = scope.ServiceProvider.GetRequiredService<IBenchmarkBestFileReader>();
                var solver = scope.ServiceProvider.GetRequiredService<ISolver>();

                if (!context.BenchmarkResults.Any())
                {
                    await SeedSolomonBenchmarks(context, fileProvider, benchmarkInstanceFileReader, solver, benchmarkBestFileReader);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task SeedSolomonBenchmarks(ApplicationDbContext context, IFileProviderService fileProviderService, IBenchmarkInstanceFileReader benchmarkFileReader,
            ISolver solver, IBenchmarkBestFileReader benchmarkBestFileReader)
        {
            var benchmarkFiles = fileProviderService.GetFiles(SolomonFiles.Instance);

            if (benchmarkFiles == null || !benchmarkFiles.Any())
                throw new ArgumentNullException();

            foreach (var file in benchmarkFiles)
            {
                var content = await File.ReadAllTextAsync(file.FullName);

                var problem = benchmarkFileReader.ReadBenchmarkFile(content);

                var solution = solver.Solve(problem);

                var bestSolution = await GetBestSolution(benchmarkBestFileReader, fileProviderService, problem, file.Name);

                var benchmark = AddBenchmarkToDb(context, file, solution, bestSolution);
            }

            await context.SaveChangesAsync();
        }

        private BenchmarkResult AddBenchmarkToDb(ApplicationDbContext context, FileInfo file, Solution solution, Solution bestSolution)
        {
            var benchmarkInstance = context.BenchmarkInstances.FirstOrDefault(x => x.Name.ToLower().Equals(Path.GetFileNameWithoutExtension(file.Name).ToLower()));

            if (benchmarkInstance == null)
                throw new ArgumentNullException();

            if (bestSolution != null)
            {
                bestSolution.Distance = benchmarkInstance.BestDistance;
                bestSolution.Time = benchmarkInstance.BestDistance;
            }

            var benchmarkResult = new BenchmarkResult()
            {
                Solution = solution,
                BenchmarkInstance = benchmarkInstance,
                BestSolution = bestSolution
            };

            context.Add(benchmarkResult);
            return benchmarkResult;
        }

        private async Task<Solution> GetBestSolution(IBenchmarkBestFileReader benchmarkBestFileReader, IFileProviderService fileProviderService, Problem problem, string fileName)
        {
            try
            {
                var file = fileProviderService.GetFile(fileName.ToLower(), SolomonFiles.Best);

                var content = await File.ReadAllTextAsync(file.FullName);

                var bestRoutes = benchmarkBestFileReader.ReadBenchmarkBestFile(content);

                List<Route> routes = new List<Route>();

                for (int i = 0; i < bestRoutes.Count; i++)
                {
                    routes.Add(CreateRoute(problem.Customers, problem.Depot, bestRoutes[i], i));
                }

                var solution = new Solution()
                {
                    Depot = problem.Depot.Clone(),
                    Feasible = true,
                    Routes = routes
                };

                return solution;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            catch (ValidationException ex)
            {
                throw;
            }
        }

        private Route CreateRoute(List<Customer> customers, Domain.Entities.Depot depot, List<int> order, int index)
        {
            List<Customer> routeCustomers = new List<Customer>();

            foreach (var id in order)
            {
                routeCustomers.Add(customers.First(x => x.Id == id));
            }

            var route = new Route()
            {
                Depot = depot,
                Id = index,
                Customers = routeCustomers
            };

            return route;
        }
    }
}