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
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, IEnumerable<BookingResponseII>>
    {
        private readonly ILogger<GetBookingsQueryHandler> _logger;
        private readonly IBookingRepository _bookingRepository;
        public GetBookingsQueryHandler(IBookingRepository bookingRepository, ILogger<GetBookingsQueryHandler> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            
        }
        public async Task<IEnumerable<BookingResponseII>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingRepository.GetBookings();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetBookingsQueryHandler: {ex.Message}");
                throw new InvalidResponseException(ex.Message);
            }
        }
    }
}