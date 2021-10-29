using AlgorithmCoreVRPTW.Models;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface IImprovement
    {
        Solution Improve(Solution currentSolution);
    }
}