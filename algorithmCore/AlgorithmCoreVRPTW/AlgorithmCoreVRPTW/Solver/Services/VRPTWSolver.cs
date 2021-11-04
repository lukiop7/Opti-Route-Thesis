using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class VRPTWSolver : ISolver
    {
        public IMethod Initial { get; set; } = new PFIHInitial();
        public IImprovement LocalSearch { get; set; } = new LocalSearchLambda();

        public Solution Create(Problem problem)
        {
            var initial = Initial.Solve(problem);
            return initial;
        }

        public Solution Improve(Solution solution)
        {
            return LocalSearch.Improve(solution);
        }

        public Solution Solve(Problem problem)
        {
            var initial = Initial.Solve(problem);
            return LocalSearch.Improve(initial);
        }
    }
}