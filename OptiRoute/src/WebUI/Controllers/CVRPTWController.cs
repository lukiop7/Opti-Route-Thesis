using AlgorithmCoreVRPTW.Solver.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Application.CVRPTW.Queries;
using System.Collections.Generic;
using System.Linq;
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
            return await Mediator.Send(new GetSolutionQuery() { Problem=problem});
        }
    }
}
