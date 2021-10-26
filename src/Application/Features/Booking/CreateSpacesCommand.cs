using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Booking
{
    public class CreateSpacesCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int AvailableSpace { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }

}