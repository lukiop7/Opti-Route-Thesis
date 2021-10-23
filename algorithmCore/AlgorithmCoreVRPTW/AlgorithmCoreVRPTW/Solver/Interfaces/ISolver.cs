using AlgorithmCoreVRPTW.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface ISolver
    {
         IInitial Initial { get; set; }
         void Solve(Problem problem);
    }
}
