using FluentValidation;
using OptiRoute.Application.CVRPTW.Commands;

namespace OptiRoute.Application.CVRPTW.Validators
{
    public class GetSolutionQueryValidator : AbstractValidator<GetSolutionCommand>
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