using OptiRoute.Domain.Entities;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface IImprovement
    {
        Solution Improve(Solution currentSolution);
    }
}