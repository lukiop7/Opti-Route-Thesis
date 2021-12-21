using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using AlgorithmCoreVRPTW.FileReaders.Services;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using FluentAssertions;
using NUnit.Framework;
using OptiRoute.Application.UnitTests.Instances;
using OptiRoute.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiRoute.Application.UnitTests.Common.Services
{
    public class PFIHInitialTests
    {
        IFileReader benchmarkReader;
        IMethod pfihMethod;
        [OneTimeSetUp]
        public void Init()
        {
            benchmarkReader = new BenchmarkFileReader();
            pfihMethod = new PFIHInitial();
        }
        [Test]
        public void ShouldSolveC101()
        {
            var benchmarkProblem = benchmarkReader.ReadBenchmark(BenchmarkInstances.C101);
            Solution solution = pfihMethod.Solve(benchmarkProblem);

            solution.Feasible.Should().BeTrue();
            solution.Distance.Should().Be(878.36);
            solution.Routes.Count.Should().Be(10);
        }


    }
}
