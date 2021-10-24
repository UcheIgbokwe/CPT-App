using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Contracts.Domain.DTO
{
    public class RegisterResponse
    {
        public RegisterResponse()
        {
        }
        public RegisterResponse(User user)
        {
            Id = user.Id;
            FullName = $"{user.FirstName} {user.LastName}";
            EmailAddress = user.Email;
            Role = user.Role;
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}