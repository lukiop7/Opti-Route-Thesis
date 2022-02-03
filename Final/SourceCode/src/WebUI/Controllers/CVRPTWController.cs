using AlgorithmCoreVRPTW.Solver.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Application.CVRPTW.Commands;
using System.Threading.Tasks;

namespace OptiRoute.WebUI.Controllers
{
    public class CVRPTWController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SolutionDto>> GetSolution(ProblemDto problem)
        {
            return await Mediator.Send(new GetSolutionCommand() { Problem = problem });
        }
    }
}