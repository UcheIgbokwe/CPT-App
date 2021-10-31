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

namespace Application.Features.Booking
{
    public class UpdateSpacesCommandHandler : IRequestHandler<UpdateSpacesCommand, LocationResponse>
    {
        private readonly ILogger<UpdateSpacesCommandHandler> _logger;
        private readonly ISpacesRepository _spaceRepository;
        public UpdateSpacesCommandHandler(ISpacesRepository spaceRepository, ILogger<UpdateSpacesCommandHandler> logger)
        {
            _spaceRepository = spaceRepository;
            _logger = logger;
            
        }
        public async Task<LocationResponse> Handle(UpdateSpacesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                return  await _spaceRepository.UpdateSpaces(command);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateSpacesCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}