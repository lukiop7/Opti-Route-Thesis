using FluentValidation;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Application.CVRPTW.Queries;

namespace OptiRoute.Application.CVRPTW.Validators
{
    public class DepotDtoValidator : AbstractValidator<DepotDto>
    {
        public DepotDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(v => v.Id)
                .NotNull()
                .GreaterThanOrEqualTo(0);

            RuleFor(v => v.X)
               .NotNull()
               .GreaterThanOrEqualTo(0);

            RuleFor(v => v.Y)
               .NotNull()
               .GreaterThanOrEqualTo(0);

            RuleFor(v => v.DueDate)
                .NotEmpty();
        }
    }
}
