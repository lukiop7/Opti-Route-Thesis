using AlgorithmCoreVRPTW.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface ISolver
    {
         IMethod Initial { get; set; }
         IMethod LocalSearch { get; set; }

         Solution Solve(Problem problem);
    }
}
