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
    public class GetBookingQueryHandler : IRequestHandler<GetBookingQuery, BookingResponseII>
    {
        private readonly ILogger<GetBookingQueryHandler> _logger;
        private readonly IBookingRepository _bookingRepository;
        public GetBookingQueryHandler(IBookingRepository bookingRepository, ILogger<GetBookingQueryHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            
        }
        public async Task<BookingResponseII> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingRepository.GetBooking(request.Email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetBookingQueryHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}