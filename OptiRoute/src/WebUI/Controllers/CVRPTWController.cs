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
        private readonly ISolver solver;
        private readonly IMapper mapper;

        public CVRPTWController(ISolver _solver, IMapper mapper)
        {
            this.solver = _solver;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SolutionDto>> GetSolution(ProblemDto problem)
        {
            return await Mediator.Send(new GetSolutionCommand() { Problem = problem });
        }
    }
}