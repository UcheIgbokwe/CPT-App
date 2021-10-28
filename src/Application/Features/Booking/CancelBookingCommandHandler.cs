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
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, bool>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CancelBookingCommandHandler> _logger;
        public CancelBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, ILogger<CancelBookingCommandHandler> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _bookingRepository = bookingRepository;
            
        }
        public async Task<bool> Handle(CancelBookingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                bool bookingResponse = _bookingRepository.CancelBooking(command);
                await _unitOfWork.CompleteAsync();
                return bookingResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CancelBookingCommandHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}