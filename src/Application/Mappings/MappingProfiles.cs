using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Application.Features.Accounts;
using Application.Features.Booking;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterUserCommand>().ReverseMap();
            CreateMap<RegisterResponse, User>().ReverseMap();
            CreateMap<Booking, CreateBookingCommand>().ReverseMap();
        }
    }
}