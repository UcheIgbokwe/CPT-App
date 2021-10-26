using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Booking
{
    public class CreateSpacesCommandHandler : IRequestHandler<CreateSpacesCommand, bool>
    {
        private readonly ISpacesRepository _spaceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSpacesCommandHandler> _logger;
        public CreateSpacesCommandHandler(ISpacesRepository spaceRepository, IUnitOfWork unitOfWork, ILogger<CreateSpacesCommandHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _spaceRepository = spaceRepository;
            
        }
        public async Task<bool> Handle(CreateSpacesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                bool locationResponse = await _spaceRepository.CreateSpaces(command);
                await _unitOfWork.CompleteAsync();
                return locationResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateSpacesCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}