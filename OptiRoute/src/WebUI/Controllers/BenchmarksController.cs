using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.Benchmarks.Commands;
using OptiRoute.Application.CVRPTW.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptiRoute.WebUI.Controllers
{
    public class BenchmarksController: ApiControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<SolutionDto>> GetSolution(IFormFile file)
        {
            return await Mediator.Send(new SolveBenchmarkProblemCommand() { File = file });
        }
    }
}
