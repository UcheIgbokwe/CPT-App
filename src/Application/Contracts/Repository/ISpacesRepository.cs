using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Booking;

namespace Application.Contracts.Repository
{
    public interface ISpacesRepository
    {
        Task<LocationResponse> CreateSpaces(CreateSpacesCommand command);
        Task<LocationResponse> UpdateSpaces(UpdateSpacesCommand command);
        Task<LocationResponse2> GetSpaces(int Id);
        
    }
}