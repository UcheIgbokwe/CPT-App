using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Transactions;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Domain.Extensions;
using Application.Contracts.Repository;
using Application.Features.Booking;
using Application.Mappings;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class SpacesRepository : GenericRepository<LocationDetail>, ISpacesRepository
    {
        public SpacesRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<LocationResponse> CreateSpaces(CreateSpacesCommand command)
        {
            try
            {
                var locationEntity = AppMapper.Mapper.Map<LocationDetail>(command);
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = _dbcontext.Users.Where(c => c.Id == command.UserId).FirstOrDefault();
                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role != Role.Admin)
                    {
                        throw new HttpStatusException(HttpStatusCode.Unauthorized, "User is not an admin.");
                    }

                    await AddAsync(locationEntity);
                    _dbcontext.SaveChanges();
                    transaction.Complete();

                    return locationEntity.ToSpaces();
                }
                catch (TransactionAbortedException ex)
                {
                    throw new InvalidResponseException(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
        public async Task<LocationResponse> UpdateSpaces(UpdateSpacesCommand command)
        {
            try
            {
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = await _dbcontext.Users.Where(c => c.Id == command.UserId).FirstOrDefaultAsync();
                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role != Role.Admin)
                    {
                        throw new HttpStatusException(HttpStatusCode.Unauthorized, "User is not an admin.");
                    }

                    var locationDetail = GetById(command.Id);
                    if(locationDetail != null)
                    {
                        locationDetail.AvailableSpace += command.AvailableSpace;
                        locationDetail.CreatedAt = DateTime.Now;

                        Update(locationDetail);
                        _dbcontext.SaveChanges();
                    }
                    else{
                        throw new HttpStatusException(HttpStatusCode.NotFound, "Location not found.");
                    } 
                    
                    transaction.Complete();

                    return locationDetail.ToSpaces();
                }
                catch (TransactionAbortedException ex)
                {
                    throw new InvalidResponseException(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
        public async Task<LocationResponseII> GetSpace(int Id)
        {
            try
            {
                var location =  await _dbcontext.LocationDetails.Where(c => c.Id == Id).Select( s => new LocationResponseII()
                {
                    Id = s.Id,
                    LocationName = s.LocationName,
                    AvailableSpace = s.AvailableSpace,
                    CreatedAt = s.CreatedAt,
                    UserId = s.CreatedBy
                    
                }).FirstOrDefaultAsync();

                return location;
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
        public async Task<IEnumerable<LocationResponse>> GetSpaces()
        {
            try
            {
                var location =  await _dbcontext.LocationDetails.Select( s => new LocationResponse()
                {
                    Id = s.Id,
                    LocationName = s.LocationName,
                    AvailableSpace = s.AvailableSpace,
                    CreatedAt = s.CreatedAt
                    
                }).ToListAsync();

                return location;
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
    }
}