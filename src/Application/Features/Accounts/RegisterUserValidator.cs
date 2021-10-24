using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Accounts
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();    
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(x => x.Role)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
        }
    }
}