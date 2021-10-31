using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider, ILogger<DataGenerator> logger)
        {
            using var dataContext = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());
            if (!dataContext.Users.Any())
            {
                dataContext.Users.AddRange(ConfigureUser());
                dataContext.SaveChanges();
                logger.LogInformation($"Seed database for User associate with {typeof(DataContext).Name} successful.");
            }
            if (!dataContext.LocationDetails.Any())
            {
                dataContext.LocationDetails.Add(ConfigureLocation());
                dataContext.SaveChanges();
                logger.LogInformation($"Seed database for Location associate with {typeof(DataContext).Name} successful.");
            }

        }
        private static IEnumerable<User> ConfigureUser()
        {
            return new List<User>
            {
                new User()
                {
                    FirstName = "Barack",
                    LastName = "Obama",
                    Gender = GenderModel.Male,
                    Role = Role.User,
                    Email = "barack.obama@gmail.com"
                },
                new User()
                {
                    FirstName = "Michelle",
                    LastName = "Obama",
                    Gender = GenderModel.Female,
                    Role = Role.User,
                    Email = "michelle.obama@gmail.com"
                },
                new User()
                {
                    FirstName = "George",
                    LastName = "Bush",
                    Gender = GenderModel.Male,
                    Role = Role.Admin,
                    Email = "george.bush@gmail.com"
                }
            };
        }
        private static LocationDetail ConfigureLocation()
        {
            return new LocationDetail
            {
                LocationName = "White House Center",
                AvailableSpace = 30,
                CreatedAt = DateTime.Now,
                CreatedBy = 3
            };
        }
    }
}