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
    public class GetLocationsQueryHandler : IRequestHandler<GetLocationsQuery, IEnumerable<LocationResponse>>
    {
        private readonly ISpacesRepository _spaceRepository;
        private readonly ILogger<GetLocationsQueryHandler> _logger;
        public GetLocationsQueryHandler(ISpacesRepository spaceRepository, ILogger<GetLocationsQueryHandler> logger)
        {
            _logger = logger;
            _spaceRepository = spaceRepository;
            
        }
        public async Task<IEnumerable<LocationResponse>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _spaceRepository.GetSpaces();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetLocationsQueryHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}