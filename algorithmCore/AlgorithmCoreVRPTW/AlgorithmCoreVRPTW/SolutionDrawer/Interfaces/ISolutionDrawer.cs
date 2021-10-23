using AlgorithmCoreVRPTW.Models;
using OptiRoute.Shared.SolutionDrawer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Shared.SolutionDrawer
{
    public interface ISolutionDrawer
    {
        DrawSolutionResponseDto DrawSolution(Solution solution, string path);
    }
}
