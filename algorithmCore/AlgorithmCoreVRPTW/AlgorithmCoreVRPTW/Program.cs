using AlgorithmCoreVRPTW.FileReaders.Interfaces;
using AlgorithmCoreVRPTW.FileReaders.Services;
using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using AlgorithmCoreVRPTW.Solver.Services;
using OptiRoute.Shared.SolutionDrawer;
using System.IO;
using System.Threading;

namespace AlgorithmCoreVRPTW
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string inputFile = @"G:\STUDIA\INŻYNIERKA\BEngThesisApp\materials\solomon\c1\c101.txt";
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

                solutionDrawer.DrawSolution(solution, outputSolutionFilePath + Path.GetFileNameWithoutExtension(path) + "\\");

                solution = solver.Solve(benchmarkProblem);
                Thread.Sleep(500);
                solutionDrawer.DrawSolution(solution, outputSolutionFilePath + Path.GetFileNameWithoutExtension(path) + "\\");
            }
        }
    }
}