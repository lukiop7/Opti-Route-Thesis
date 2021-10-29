using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class VRPTWSolver : ISolver
    {
        public IMethod Initial { get; set; } = new ClarkeWrightInitial();
        public IImprovement LocalSearch { get; set; } = new LocalSearchLambda();

        public Solution Create(Problem problem)
        {
           var initial= Initial.Solve(problem);
            if (!initial.Feasible)
                throw new Exception("Chuj");
            return initial;
        }

        public Solution Solve(Problem problem)
        {
            var initial = Initial.Solve(problem);
            if (!initial.Feasible)
                throw new Exception("Chuj");
            return LocalSearch.Improve(initial);
        }
    }
}