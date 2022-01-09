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

namespace OptiRoute.Application.Benchmarks.Queries.GetBenchmarkResultByIdQuery
{
    public class GetBenchmarkResultByIdQuery : IRequest<BenchmarkResultDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetBenchmarkResultByIdQueryHandler : IRequestHandler<GetBenchmarkResultByIdQuery, BenchmarkResultDetailsDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBenchmarkResultByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BenchmarkResultDetailsDto> Handle(GetBenchmarkResultByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.BenchmarkResults
                .Include(x => x.BenchmarkInstance)
                .Include(x => x.Solution)
                .ThenInclude(x => x.Depot)
                .Include(x => x.Solution)
                .ThenInclude(x => x.Routes)
                .ThenInclude(x => x.Customers)
                .FirstOrDefaultAsync(x=> x.DbId == request.Id);

            return _mapper.Map<BenchmarkResultDetailsDto>(result);
        }
    }
}
