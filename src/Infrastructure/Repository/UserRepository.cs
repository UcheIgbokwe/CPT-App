using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Application.Behaviours;
using Application.Contracts.Domain.DTO;
using Application.Contracts.Domain.Extensions;
using Application.Contracts.Repository;
using Application.Features.Accounts;
using Application.Mappings;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<RegisterResponse> RegisterUser(RegisterUserCommand command)
        {
            try
            {
                var userEntity = AppMapper.Mapper.Map<User>(command);
                try
                {
                    using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                    await AddAsync(userEntity);
                    
                    transaction.Complete();
                    return userEntity.ToUser();
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
        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            try
            {
                var users =  await _dbcontext.Users.Select( s => new UserResponse()
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Gender = s.Gender,
                    Email = s.Email,
                    Role = s.Role
                }).ToListAsync();

                return users;

            }
            catch (Exception ex)
            {
                throw new HandleDbException(ex.Message);
            }
        }
    }
}