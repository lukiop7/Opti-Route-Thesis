using OptiRoute.Domain.Entities;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface ISolver
    {
        IMethod Initial { get; set; }
        IImprovement Improvement { get; set; }

        Solution Solve(Problem problem);

        Solution Create(Problem problem);

        Solution Improve(Solution solution);
    }
}