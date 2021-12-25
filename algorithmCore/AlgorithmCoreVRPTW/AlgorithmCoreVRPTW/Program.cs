using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using AlgorithmCoreVRPTW.FileReaders.Services;
using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using OptiRoute.Shared.SolutionDrawer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AlgorithmCoreVRPTW
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string inputFile = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\materials\solomon\c1\c101_25.txt";
            string[] filePaths = Directory.GetFiles(@"G:\STUDIA\INŻYNIERKA\BEngThesisApp\materials\solomon\c1\", "*.txt",
                                         SearchOption.TopDirectoryOnly);
            string outputSolutionFilePath = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\Results\";

            ISolutionDrawer solutionDrawer = new SolutionDrawer();
            IFileReader fileReader = new BenchmarkFileReader();
            ISolver solver = new VRPTWSolver();

            foreach (var path in filePaths)
            {
                var benchmarkProblem = fileReader.ReadBenchmarkFile(path);
                List<List<double>> distances = new List<List<double>>();
                List<List<double>> durations = new List<List<double>>();

                List<double> depotDistances = new List<double>();
                List<double> depotDurations = new List<double>();
                depotDistances.Add(0);
                depotDurations.Add(0);
                for (int i = 0; i < benchmarkProblem.Customers.Count; i++)
                {
                    depotDistances.Add(benchmarkProblem.Customers[i].CalculateDistanceBetween(benchmarkProblem.Depot));
                    depotDurations.Add(benchmarkProblem.Customers[i].CalculateDistanceBetween(benchmarkProblem.Depot));
                }
                distances.Add(depotDistances);
                durations.Add(depotDurations);
                foreach (var customer in benchmarkProblem.Customers)
                {
                    List<double> customerDistances = new List<double>();
                    List<double> customerDurations = new List<double>();
                    customerDistances.Add(customer.CalculateDistanceBetween(benchmarkProblem.Depot));
                    customerDurations.Add(customer.CalculateDistanceBetween(benchmarkProblem.Depot));
                    for(int i = 0; i < benchmarkProblem.Customers.Count; i++)
                    {
                        if(benchmarkProblem.Customers[i].Id == customer.Id)
                        {
                            customerDistances.Add(0);
                            customerDurations.Add(0);
                        }
                        else
                        {
                            customerDistances.Add(customer.CalculateDistanceBetween(benchmarkProblem.Customers[i]));
                            customerDurations.Add(customer.CalculateDistanceBetween(benchmarkProblem.Customers[i]));
                        }
                    }
                    distances.Add(customerDistances);
                    durations.Add(customerDurations);
                }
                benchmarkProblem.Distances = distances;
                benchmarkProblem.Durations = durations;
                Solution solution = solver.Create(benchmarkProblem);

                if (solution.Feasible)
                {
                  solutionDrawer.DrawSolution(solution, outputSolutionFilePath + Path.GetFileNameWithoutExtension(path) + "\\", "_initial");
                    solution = solver.Improve(solution);
                    if (solution.Feasible) 
                        solutionDrawer.DrawSolution(solution, outputSolutionFilePath + Path.GetFileNameWithoutExtension(path) + "\\", "_improved");
                    else
                        Console.WriteLine(path + " IMPROVED");
                }
                else
                    Console.WriteLine(path);
            }
        }
    }
}