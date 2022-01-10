using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OptiRoute.Application.Common.Interfaces;
using OptiRoute.Application.CVRPTW.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.Benchmarks.Queries.GetSolutionByBenchmarkResultIdQuery
{
    public class GetSolutionByBenchmarkResultIdQuery : IRequest<SolutionDto>
    {
        public int BenchmarkResultId { get; set; }
    }

    public class GetSolutionByBenchmarkResultIdQueryHandler : IRequestHandler<GetSolutionByBenchmarkResultIdQuery, SolutionDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSolutionByBenchmarkResultIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SolutionDto> Handle(GetSolutionByBenchmarkResultIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Solutions
                .Include(x => x.Depot)
                .Include(x => x.Routes)
                .ThenInclude(x => x.Customers)
                .Include(x => x.BenchmarkResult)
                .FirstOrDefaultAsync(x => x.BenchmarkResult.DbId == request.BenchmarkResultId);

            return _mapper.Map<SolutionDto>(result);
        }
    }
}