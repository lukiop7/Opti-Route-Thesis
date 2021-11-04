﻿using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using AlgorithmCoreVRPTW.FileReaders.Services;
using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using OptiRoute.Shared.SolutionDrawer;
using System;
using System.IO;

namespace AlgorithmCoreVRPTW
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string inputFile = @"G:\\STUDIA\\INŻYNIERKA\\BEngThesisApp\\materials\\solomon\\c1\\c101.txt";
            string[] filePaths = Directory.GetFiles(@"G:\STUDIA\INŻYNIERKA\BEngThesisApp\materials\solomon\c1\", "*.txt",
                                         SearchOption.TopDirectoryOnly);
            string outputSolutionFilePath = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\Results\";

            ISolutionDrawer solutionDrawer = new SolutionDrawer();
            IFileReader fileReader = new BenchmarkFileReader();
            ISolver solver = new VRPTWSolver();

            foreach (var path in filePaths)
            {
                var benchmarkProblem = fileReader.ReadBenchmarkFile(path);

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