using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Domain.Entities;
using OptiRoute.Infrastructure.FileReaders.Services;
using OptiRoute.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
           using(var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProviderService>();
                var benchmarkFileReader = scope.ServiceProvider.GetRequiredService<IBenchmarkFileReader>();
                var solver = scope.ServiceProvider.GetRequiredService<ISolver>();

                if (!context.BenchmarkResults.Any())
                {
                    await SeedSolomonBenchmarks(context, fileProvider, benchmarkFileReader, solver);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task SeedSolomonBenchmarks(ApplicationDbContext context, IFileProviderService fileProviderService, IBenchmarkFileReader benchmarkFileReader,
            ISolver solver)
        {
            var benchmarkFiles = fileProviderService.GetFiles();

            if (benchmarkFiles == null || !benchmarkFiles.Any())
                throw new ArgumentNullException();

            foreach(var file in benchmarkFiles)
            {
                var content = await File.ReadAllTextAsync(file.FullName);

                var problem = benchmarkFileReader.ReadBenchmarkFile(content);

                var solution = solver.Solve(problem);

                AddBenchmarkToDb(context, file, solution);
            }

            await context.SaveChangesAsync();
        }

        private void AddBenchmarkToDb(ApplicationDbContext context, FileInfo file, Solution solution)
        {
            var benchmarkInstance = context.BenchmarkInstances.FirstOrDefault(x => x.Name.ToLower().Equals(Path.GetFileNameWithoutExtension(file.Name).ToLower()));

            if (benchmarkInstance == null)
                throw new ArgumentNullException();

            var benchmarkResult = new BenchmarkResult()
            {
                Solution = solution,
                BenchmarkInstance = benchmarkInstance
            };

            context.Add(benchmarkResult);
        }
    }
}
