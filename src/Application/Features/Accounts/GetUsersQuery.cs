using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Accounts
{
    public class GetUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
        
    }
}