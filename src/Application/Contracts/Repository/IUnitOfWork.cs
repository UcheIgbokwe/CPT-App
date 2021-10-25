using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Contracts.Repository
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IBookingRepository Bookings { get; }
        Task CompleteAsync();
    }
}