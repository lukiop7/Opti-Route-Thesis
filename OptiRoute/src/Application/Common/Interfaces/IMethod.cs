using AlgorithmCoreVRPTW.Models;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface IMethod
    {
        Solution Solve(Problem problem);
    }
}