using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Booking
{
    public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidator()
        {
            RuleFor(x => x.TestDate)
                .NotEmpty().WithMessage("*Required")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Please enter a present or future date.");
        }
    }
}