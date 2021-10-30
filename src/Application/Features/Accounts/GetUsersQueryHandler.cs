using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Accounts
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUsersQueryHandler> _logger;
        public GetUsersQueryHandler(IUserRepository userRepository, ILogger<GetUsersQueryHandler> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetReportQueryHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}