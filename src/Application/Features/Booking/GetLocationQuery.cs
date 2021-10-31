using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using MediatR;

namespace Application.Features.Booking
{
    public class GetLocationQuery : IRequest<LocationResponse2>
    {
        public int Id { get; set; }

        public GetLocationQuery(int id)
        {
            Id = id;
        }
    }
}