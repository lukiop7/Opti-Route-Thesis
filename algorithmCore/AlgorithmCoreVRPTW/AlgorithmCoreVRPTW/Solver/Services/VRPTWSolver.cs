using AlgorithmCoreVRPTW.Models;
using AlgorithmCoreVRPTW.Solver.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Services
{
    public class VRPTWSolver : ISolver
    {
        public IInitial Initial { get; set; } = new ClarkeWrightInitial(); 

        public void Solve(Problem problem)
        {
            Initial.Solve(problem);
        }
    }
}
