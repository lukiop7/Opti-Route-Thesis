using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using AlgorithmCoreVRPTW.FileReaders.Services;
using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using OptiRoute.Shared.SolutionDrawer;
using System;
using System.Linq;

namespace AlgorithmCoreVRPTW
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\materials\solomon\c1\c101.txt";
            string outputSolutionFilePath = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\Results\";

            ISolutionDrawer solutionDrawer = new SolutionDrawer();
            IFileReader fileReader = new BenchmarkFileReader();
            ISolver solver = new VRPTWSolver();

            var benchmarkProblem = fileReader.ReadBenchmarkFile(inputFile);

            Solution solution = solver.Solve(benchmarkProblem);
            

            //Solution solution = new Solution()
            //{
            //    Depot = benchmarkProblem.Depot,
            //    Feasible = true,
            //};
            //solution.Routes.Add(new Route { Distance = int.MaxValue, Customers = benchmarkProblem.Customers.Take(benchmarkProblem.Customers.Count/2).ToList() });
            //solution.Routes.Add(new Route { Distance = int.MaxValue, Customers = benchmarkProblem.Customers.Skip(benchmarkProblem.Customers.Count / 2).ToList() });

            solutionDrawer.DrawSolution(solution, outputSolutionFilePath);
        }
    }
}
