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
        Task<bool> CreateSpaces(CreateSpacesCommand command);
        bool UpdateSpaces(UpdateSpacesCommand command);
        
    }
}