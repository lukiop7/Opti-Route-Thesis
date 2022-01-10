using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptiRoute.Application.Benchmarks.Commands;
using OptiRoute.Application.Benchmarks.Queries.GetBenchmarkResultsQuery;
using OptiRoute.Application.Benchmarks.Queries.GetBestSolutionByBenchmarkResultIdQuery;
using OptiRoute.Application.Benchmarks.Queries.GetSolutionByBenchmarkResultIdQuery;
using OptiRoute.Application.CVRPTW.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OptiRoute.WebUI.Controllers
{
    public class BenchmarksController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<SolutionDto>> GetSolution(IFormFile file)
        {
            return await Mediator.Send(new SolveBenchmarkProblemCommand() { File = file });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BenchmarkResultDto>>> GetBenchmarkResults()
        {
            return await Mediator.Send(new GetBenchmarkResultsQuery());
        }

        [HttpGet("{benchmarkResultId}/Solution")]
        public async Task<ActionResult<SolutionDto>> GetSolutionByBenchmarkResultId([FromRoute] int benchmarkResultId)
        {
            return await Mediator.Send(new GetSolutionByBenchmarkResultIdQuery() { BenchmarkResultId = benchmarkResultId });
        }

        [HttpGet("{benchmarkResultId}/BestSolution")]
        public async Task<ActionResult<SolutionDto>> GetBestSolutionByBenchmarkResultId([FromRoute] int benchmarkResultId)
        {
            return await Mediator.Send(new GetBestSolutionByBenchmarkResultIdQuery() { BenchmarkResultId = benchmarkResultId });
        }
    }
}