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

namespace Infrastructure.Repository
{
    public class SpacesRepository : GenericRepository<LocationDetail>, ISpacesRepository
    {
        public SpacesRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CreateSpaces(CreateSpacesCommand command)
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
                    transaction.Complete();

                    return true;
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
        public bool UpdateSpaces(UpdateSpacesCommand command)
        {
            try
            {
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

                    var locationDetail = GetById(command.Id);
                    if(locationDetail != null)
                    {
                        locationDetail.AvailableSpace += command.AvailableSpace;
                        locationDetail.CreatedAt = DateTime.Now;

                        Update(locationDetail);
                    }
                    else{
                        throw new HttpStatusException(HttpStatusCode.NotFound, "Location not found.");
                    } 
                    
                    transaction.Complete();

                    return true;
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
    }
}