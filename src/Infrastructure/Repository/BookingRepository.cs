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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public bool CancelBooking(CancelBookingCommand command)
        {
            try
            {
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = _dbcontext.Users.Where(c => c.Email == command.Email).FirstOrDefault();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role != Role.User)
                    {
                        throw new HttpStatusException(HttpStatusCode.Unauthorized, "Booking is only available to users.");
                    }

                    //check if user has existing open booking.
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email == user.Email && c.Status == StatusModel.Pending).FirstOrDefault();

                    if(existingBooking == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "No pending booking.");
                    }

                    var availableSpace = _dbcontext.LocationDetails.Where(c => c.Id == existingBooking.LocationId).FirstOrDefault();

                    existingBooking.Status = StatusModel.Cancelled;
                    existingBooking.UpdateDate = DateTime.Now;
                    Update(existingBooking);

                    //increase the available space
                    availableSpace.AvailableSpace++;
                    _dbcontext.LocationDetails.Update(availableSpace);
                    
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
        public async Task<BookingResponse> CreateBooking(CreateBookingCommand command)
        {
            try
            {
                var bookEntity = AppMapper.Mapper.Map<Booking>(command);
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = _dbcontext.Users.Where(c => c.Email == bookEntity.Email).FirstOrDefault();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role != Role.User)
                    {
                        throw new HttpStatusException(HttpStatusCode.Unauthorized, "Booking is only available to users.");
                    }

                    //check if space is available
                    var availableSpace = _dbcontext.LocationDetails.Where(c => c.Id == bookEntity.LocationId).FirstOrDefault();

                    if(availableSpace == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "Location is invalid.");
                    }
                    if(availableSpace.AvailableSpace < 1)
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "There is no available space.");
                    }

                    //check if user has existing open booking.
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email == user.Email && c.Status == StatusModel.Pending).FirstOrDefault();

                    if(existingBooking != null)
                    {
                        throw new HttpStatusException(HttpStatusCode.BadRequest, "User has an existing booking.");
                    }

                    await AddAsync(bookEntity);

                    //reduce the available space
                    availableSpace.AvailableSpace--;
                    _dbcontext.LocationDetails.Update(availableSpace);
                    
                    transaction.Complete();
                    return bookEntity.ToBooking();
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
        public bool UpdateTestResult(UpdateTestCommand command)
        {
            try
            {
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = _dbcontext.Users.Where(c => c.Email == command.Email).FirstOrDefault();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }

                    //check if user has existing open booking.
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email == user.Email && c.Status == StatusModel.Pending).FirstOrDefault();

                    if(existingBooking == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "No pending booking.");
                    }

                    var availableSpace = _dbcontext.LocationDetails.Where(c => c.Id == existingBooking.LocationId).FirstOrDefault();

                    existingBooking.Status = StatusModel.Closed;
                    existingBooking.TestResult = command.TestResult.ToLower();
                    existingBooking.UpdateDate = DateTime.Now;
                    Update(existingBooking);
                    
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