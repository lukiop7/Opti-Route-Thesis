using FluentValidation;
using OptiRoute.Application.CVRPTW.Queries;

namespace OptiRoute.Application.CVRPTW.Validators
{
    public class GetSolutionQueryValidator : AbstractValidator<GetSolutionQuery>
    {
        public GetSolutionQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(v => v.Problem)
                .NotEmpty()
                .SetValidator(new ProblemDtoValidator());
        }
    }
}