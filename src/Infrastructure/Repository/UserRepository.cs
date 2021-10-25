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
                    _logger.LogInformation("Log works");
                    return userEntity.ToUser();
                }
                catch (TransactionAbortedException ex)
                {
                    _logger.LogError($"Error in UserRepository.RegisterUser: {ex.Message}");
                    throw new InvalidResponseException(ex.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UserRepository.RegisterUser: {ex.Message}");
                throw new HandleDbException(ex.Message);
            }
        }
    }
}