using FluentValidation;
using OptiRoute.Application.CVRPTW.Dtos;
using OptiRoute.Application.CVRPTW.Queries;
using System;

namespace OptiRoute.Application.CVRPTW.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
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

            RuleFor(v => v.Demand)
              .NotNull()
              .GreaterThan(0);

            RuleFor(v => v.ReadyTime)
               .NotNull()
               .GreaterThanOrEqualTo(0);

            RuleFor(v => v.DueDate)
               .NotEmpty()
               .GreaterThan(v => v.ReadyTime)
               .WithMessage(v => string.Format("Customer {0} - Due date must be greater than Ready time", v.Id));

            RuleFor(v => v.ServiceTime)
              .NotEmpty()
              .GreaterThan(0)
               .WithMessage(v => string.Format("Customer {0} - Service time must be greater than 00:00", v.Id));
        }
    }
}
