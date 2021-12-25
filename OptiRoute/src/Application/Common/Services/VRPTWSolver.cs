using AlgorithmCoreVRPTW.Solver.Interfaces;
using OptiRoute.Domain.Entities;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class VRPTWSolver : ISolver
    {
        public IMethod Initial { get; set; }
        public IImprovement Improvement { get; set; }

       public VRPTWSolver(IMethod Initial, IImprovement LocalSearch)
        {
            this.Initial = Initial;
            this.Improvement = LocalSearch;
        }
        public Solution Create(Problem problem)
        {
            var initial = Initial.Solve(problem);
            return initial;
        }

        public Solution Improve(Solution solution)
        {
            return Improvement.Improve(solution);
        }

        public Solution Solve(Problem problem)
        {
            var initial = Initial.Solve(problem);
            if(initial.Feasible)
                return Improvement.Improve(initial);

            return initial;
        }
    }
}