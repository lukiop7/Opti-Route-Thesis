using OptiRoute.Shared.SolutionDrawer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OptiRoute.Shared.SolutionDrawer
{
    public interface ISolutionDrawer
    {
        DrawSolutionResponseDto DrawSolution(List<(int x, int y)> points, List<int> route, string path);
    }
}
