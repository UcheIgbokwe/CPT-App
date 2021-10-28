using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Booking;

namespace Application.Contracts.Repository
{
    public interface IBookingRepository 
    {
        Task<BookingResponse> CreateBooking(CreateBookingCommand command);
        bool CancelBooking(CancelBookingCommand command);
        bool UpdateTestResult(UpdateTestCommand command);
    }
}