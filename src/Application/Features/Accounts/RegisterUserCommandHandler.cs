using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using Application.Contracts.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Accounts
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, CommandResponse>
    {
        private readonly ILogger<RegisterUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<RegisterUserCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _logger = logger;
            
        }

        public async Task<CommandResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                CommandResponse response = new();
                RegisterResponse registerResponse = await _userRepository.RegisterUser(command);
                await _unitOfWork.CompleteAsync();
                response.Response = registerResponse ?? throw new HttpStatusException(HttpStatusCode.Conflict, "Error creating user.");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in RegisterUserCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}