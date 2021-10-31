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
    public class CreateSpacesCommandHandler : IRequestHandler<CreateSpacesCommand, LocationResponse>
    {
        private readonly ISpacesRepository _spaceRepository;
        private readonly ILogger<CreateSpacesCommandHandler> _logger;
        public CreateSpacesCommandHandler(ISpacesRepository spaceRepository, ILogger<CreateSpacesCommandHandler> logger)
        {
            _logger = logger;
            _spaceRepository = spaceRepository;
            
        }
        public async Task<LocationResponse> Handle(CreateSpacesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return await _spaceRepository.CreateSpaces(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateSpacesCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}