using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Accounts
{
    public class RegisterUserCommand : IRequest<CommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

    }

    public class CommandResponse
    {
        public RegisterResponse Response { get; set; }
    }
}