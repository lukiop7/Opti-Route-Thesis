﻿using AlgorithmCoreVRPTW.Solver.Interfaces;
using AutoMapper;
using MediatR;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.CVRPTW.Commands
{
    public class GetSolutionCommand : IRequest<SolutionDto>
    {
        public ProblemDto Problem { get; set; }
    }

    public class GetSolutionCommandHandler : IRequestHandler<GetSolutionCommand, SolutionDto>
    {
        private readonly ISolver _solver;
        private readonly IMapper _mapper;

        public GetSolutionCommandHandler(ISolver solver, IMapper mapper)
        {
            _solver = solver;
            _mapper = mapper;
        }

        public async Task<SolutionDto> Handle(GetSolutionCommand request, CancellationToken cancellationToken)
        {
            Problem problemMapped = _mapper.Map<ProblemDto, Problem>(request.Problem);

            var solution = this._solver.Solve(problemMapped);
            return _mapper.Map<Solution, SolutionDto>(solution);
        }
    }
}