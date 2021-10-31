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
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<BookingResponseII> CancelBooking(CancelBookingCommand command)
        {
            try
            {
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = await _dbcontext.Users.Where(c => c.Email.ToLower() == command.Email).FirstOrDefaultAsync();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role.ToLower() != Role.User)
                    {
                        throw new HttpStatusException(HttpStatusCode.Unauthorized, "Booking is only available to users.");
                    }

                    //check if user has existing open booking.
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email.ToLower() == user.Email && c.Status.ToLower() == StatusModel.Pending).FirstOrDefault();

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

                    return existingBooking.ToBookingII();
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
                    var user = _dbcontext.Users.Where(c => c.Email.ToLower() == bookEntity.Email).FirstOrDefault();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }
                    if(user.Role.ToLower() != Role.User)
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
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email.ToLower() == user.Email && c.Status.ToLower() == StatusModel.Pending).FirstOrDefault();

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
        public async Task<BookingResponseII> GetBooking(string email)
        {
            try
            {
                var booking =  await _dbcontext.Bookings.Where(c => c.Email.ToLower() == email).Select( s => new BookingResponseII()
                {
                    Email = s.Email,
                    Status = s.Status,
                    TestResult = s.TestResult
                }).FirstOrDefaultAsync();

                return booking;
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
        public async Task<IEnumerable<BookingResponseII>> GetBookings()
        {
            try
            {
                var booking =  await _dbcontext.Bookings.Select( s => new BookingResponseII()
                {
                    Email = s.Email,
                    Status = s.Status,
                    TestResult = s.TestResult
                    
                }).ToListAsync();

                return booking;
            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
        public async Task<BookingResponseII> UpdateTestResult(UpdateTestCommand command)
        {
            try
            {
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = await _dbcontext.Users.Where(c => c.Email.ToLower() == command.Email).FirstOrDefaultAsync();

                    if(user == null)
                    {
                        throw new HttpStatusException(HttpStatusCode.NotFound, "User is invalid.");
                    }

                    //check if user has existing open booking.
                    var existingBooking = _dbcontext.Bookings.Where(c => c.Email.ToLower() == user.Email && c.Status.ToLower() == StatusModel.Pending).FirstOrDefault();

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

                    return existingBooking.ToBookingII();
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