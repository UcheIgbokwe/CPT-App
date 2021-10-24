using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.DTO;
using Domain.Models;

namespace Application.Contracts.Domain.Extensions
{
    public static class UserManager
    {
        public static RegisterResponse ToUser(this User user)
        {
            RegisterResponse newUser = new();
            newUser.FullName = $"{user.FirstName} {user.LastName}";
            newUser.EmailAddress = user.Email;
            newUser.Role = user.Role;

            return newUser;
        }
    }
}