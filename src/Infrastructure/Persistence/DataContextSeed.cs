using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence
{
    public class DataContextSeed
    {
        public static async Task SeedAsync(DataContext dataContext, ILogger<DataContextSeed> logger)
        {
            if (!dataContext.Users.Any())
            {
                dataContext.Users.AddRange(ConfigureUser());
                await dataContext.SaveChangesAsync();
                logger.LogInformation($"Seed database for User associate with {typeof(DataContext).Name} successful.");
            }
            if (!dataContext.LocationDetails.Any())
            {
                dataContext.LocationDetails.Add(ConfigureLocation());
                await dataContext.SaveChangesAsync();
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
                    Role = Role.User,
                    Email = "barack.obama@gmail.com"
                },
                new User()
                {
                    FirstName = "Michelle",
                    LastName = "Obama",
                    Role = Role.User,
                    Email = "michelle.obama@gmail.com"
                }
            };
        }
        private static LocationDetail ConfigureLocation()
        {
            return new LocationDetail
            {
                LocationName = "White House Center",
                AvailableSpace = 30
            };
        }
    }
}