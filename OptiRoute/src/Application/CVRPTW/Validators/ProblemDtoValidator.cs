using FluentValidation;
using OptiRoute.Application.CVRPTW.Dtos;

namespace OptiRoute.Application.CVRPTW.Validators
{
    public class ProblemDtoValidator : AbstractValidator<ProblemDto>
    {
        public ProblemDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(v => v.Vehicles)
                .NotNull()
                .GreaterThan(0);

            RuleFor(v => v.Capacity)
               .NotNull()
               .GreaterThan(0);

            RuleFor(v => v.Depot)
               .NotNull()
               .SetValidator(new DepotDtoValidator());

            RuleFor(v => v.Customers)
               .NotEmpty();

            RuleForEach(v => v.Customers)
               .SetValidator(new CustomerDtoValidator());

            RuleFor(v => v.Distances)
               .NotEmpty();

            RuleFor(v => v.Durations)
               .NotEmpty();
        }
    }
}