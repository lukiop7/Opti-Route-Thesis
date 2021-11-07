using AlgorithmCoreVRPTW.Models;
using OptiRoute.Shared.SolutionDrawer.Models;

namespace OptiRoute.Shared.SolutionDrawer
{
    public interface ISolutionDrawer
    {
        DrawSolutionResponseDto DrawSolution(Solution solution, string path, string word);
    }
}