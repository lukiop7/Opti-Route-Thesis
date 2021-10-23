using AlgorithmCoreVRPTW.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmCoreVRPTW.Solver.Interfaces
{
    public interface IInitial
    {
        Solution Solve(Problem problem);
    }
}
