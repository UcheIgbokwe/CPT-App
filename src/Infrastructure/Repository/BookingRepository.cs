using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<BookingResponse> CreateBooking(CreateBookingCommand command)
        {
            try
            {
                var bookEntity = AppMapper.Mapper.Map<Booking>(command);
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    var user = _dbcontext.Users.Where(c => c.Id == bookEntity.UserId).FirstOrDefault();

                    if(user == null)
                    {
                        throw new HttpStatusException("User is invalid");
                    }
                    await AddAsync(bookEntity);
                    
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
    }
}