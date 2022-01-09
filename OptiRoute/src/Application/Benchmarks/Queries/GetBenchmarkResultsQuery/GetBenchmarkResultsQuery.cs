using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OptiRoute.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Queries.GetBenchmarkResultsQuery
{
    public class GetBenchmarkResultsQuery : IRequest<List<BenchmarkResultDto>>
    {
    }

    public class GetBenchmarkResultsQueryHandler : IRequestHandler<GetBenchmarkResultsQuery, List<BenchmarkResultDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBenchmarkResultsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BenchmarkResultDto>> Handle(GetBenchmarkResultsQuery request, CancellationToken cancellationToken)
        {
            var results = await _context.BenchmarkResults
                 .Include(x => x.Solution)
                 .ThenInclude(x => x.Routes)
                .Include(x => x.BenchmarkInstance)
                .OrderBy(x => x.BenchmarkInstance.Name)
                .ToListAsync();
            return _mapper.Map<List<BenchmarkResultDto>>(results);
        }
    }
}
