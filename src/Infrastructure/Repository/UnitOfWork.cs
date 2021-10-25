using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Repository;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dbContext;
        public IUserRepository Users { get; set; }

        public IBookingRepository Bookings { get; set; }

        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;

            Users = new UserRepository(_dbContext);
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}