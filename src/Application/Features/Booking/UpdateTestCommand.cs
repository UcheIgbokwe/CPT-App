using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Booking
{
    public class UpdateTestCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Status { get; set; }
    }
}