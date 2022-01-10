using FluentValidation;

namespace OptiRoute.Application.Benchmarks.Queries.GetSolutionByBenchmarkResultIdQuery
{
    public class GetSolutionByBenchmarkResultIdQueryValidator : AbstractValidator<GetSolutionByBenchmarkResultIdQuery>
    {
        public GetSolutionByBenchmarkResultIdQueryValidator()
        {
            RuleFor(x => x.BenchmarkResultId)
                .NotEmpty();
        }
    }
}