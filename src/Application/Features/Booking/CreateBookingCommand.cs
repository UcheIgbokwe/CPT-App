using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Booking
{
    public class CreateBookingCommand : IRequest<CommandResponse>
    {
        public string Email { get; set; }
        public int LocationId { get; set; }
        public string TestType { get; set; }
        public DateTime TestDate { get; set; } 
    }

    public class CommandResponse
    {
        public BookingResponse Response { get; set; }
    }
}