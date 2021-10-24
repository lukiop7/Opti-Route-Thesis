using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class VRPTWSolver : ISolver
    {
        public IMethod Initial { get; set; } = new ClarkeWrightInitial(); 

        public Solution Solve(Problem problem)
        {
          return Initial.Solve(problem);
        }
    }
}
