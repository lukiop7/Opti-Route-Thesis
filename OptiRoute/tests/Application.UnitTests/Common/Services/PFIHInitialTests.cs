using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using OptiRoute.Infrastructure.FileReaders.Services;
using FluentAssertions;
using NUnit.Framework;
using OptiRoute.Application.UnitTests.Instances;
using OptiRoute.Domain.Entities;
using OptiRoute.Application.Common.Interfaces;

namespace OptiRoute.Application.UnitTests.Common.Services
{   
    public class PFIHInitialTests
    {
        IBenchmarkInstanceFileReader benchmarkReader;
        IMethod pfihMethod;
        [OneTimeSetUp]
        public void Init()
        {
            benchmarkReader = new BenchmarkInstanceFileReader();
            pfihMethod = new PFIHInitial();
        }
        [Test]
        public void ShouldSolveC101()
        {
            var benchmarkProblem = benchmarkReader.ReadBenchmarkFile(BenchmarkInstances.C101);
            Solution solution = pfihMethod.Solve(benchmarkProblem);

            solution.Feasible.Should().BeTrue();
            solution.Distance.Should().Be(878.36);
            solution.Routes.Count.Should().Be(10);
        }


    }
}
