using AlgorithmCoreVRPTW.Models;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface ISolver
    {
        IMethod Initial { get; set; }
        IImprovement LocalSearch { get; set; }

        Solution Solve(Problem problem);

        Solution Create(Problem problem);
    }
}