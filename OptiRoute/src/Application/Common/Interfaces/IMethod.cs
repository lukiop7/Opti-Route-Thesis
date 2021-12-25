
using OptiRoute.Domain.Entities;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface IMethod
    {
        Solution Solve(Problem problem);
    }
}