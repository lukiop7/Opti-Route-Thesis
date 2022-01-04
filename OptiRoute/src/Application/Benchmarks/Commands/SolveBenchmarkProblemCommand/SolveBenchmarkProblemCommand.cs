using AlgorithmCoreVRPTW.Solver.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OptiRoute.Application.Common.Extensions;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Commands
{
    public class SolveBenchmarkProblemCommand : IRequest<SolutionDto>
    {
        public IFormFile File { get; set; }
    }

    public class SolveBenchmarkProblemCommandHandler : IRequestHandler<SolveBenchmarkProblemCommand, SolutionDto>
    {
        private readonly ISolver _solver;
        private readonly IMapper _mapper;
        private readonly IBenchmarkFileReader _benchmarkFileReader;

        public SolveBenchmarkProblemCommandHandler(ISolver solver, IMapper mapper, IBenchmarkFileReader benchmarkFileReader)
        {
            _solver = solver;
            _mapper = mapper;
            _benchmarkFileReader = benchmarkFileReader;
        }

        public async Task<SolutionDto> Handle(SolveBenchmarkProblemCommand request, CancellationToken cancellationToken)
        {
            var content = await request.File.ReadAsStringAsync();
            Problem problemMapped = _benchmarkFileReader.ReadBenchmarkFile(content);

             var solution = this._solver.Solve(problemMapped);
            return _mapper.Map<Solution, SolutionDto>(solution);
        }
    }
}
