using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;

namespace Application.Contracts.Services
{
    public interface IUserManager
    {
        Task<RegisterResponse> RegisterUser(RegisterUserCommand command);
    }
}