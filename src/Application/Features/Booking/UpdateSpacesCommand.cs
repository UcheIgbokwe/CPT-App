using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Booking
{
    public class UpdateSpacesCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int AvailableSpace { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
    }
}