using FluentValidation;

namespace OptiRoute.Application.Benchmarks.Queries.GetBestSolutionByBenchmarkResultIdQuery
{
    public class GetBestSolutionByBenchmarkResultIdQueryValidator : AbstractValidator<GetBestSolutionByBenchmarkResultIdQuery>
    {
        public GetBestSolutionByBenchmarkResultIdQueryValidator()
        {
            RuleFor(x => x.BenchmarkResultId)
                .NotEmpty();
        }
    }
}