using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;
using Domain.Models;

namespace Application.Contracts.Repository
{
    public interface IUserRepository
    {
        Task<RegisterResponse> RegisterUser(RegisterUserCommand command);
        Task<IEnumerable<UserResponse>> GetAllUsers();
    }
}