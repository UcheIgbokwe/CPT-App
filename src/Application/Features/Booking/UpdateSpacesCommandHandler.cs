using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Behaviours;
using Application.Contracts.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Booking
{
    public class UpdateSpacesCommandHandler : IRequestHandler<UpdateSpacesCommand, bool>
    {
        private readonly ILogger<UpdateSpacesCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISpacesRepository _spaceRepository;
        public UpdateSpacesCommandHandler(ISpacesRepository spaceRepository, IUnitOfWork unitOfWork, ILogger<UpdateSpacesCommandHandler> logger)
        {
            _spaceRepository = spaceRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            
        }
        public async Task<bool> Handle(UpdateSpacesCommand command, CancellationToken cancellationToken)
        {
            try
            {
                bool locationResponse =  _spaceRepository.UpdateSpaces(command);
                await _unitOfWork.CompleteAsync();
                return locationResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateSpacesCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}