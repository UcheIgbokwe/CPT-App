using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Domain.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private ConnectionStrings _configs;
        public DataContextFactory(IOptions<ConnectionStrings> opts)
        {
            _configs = opts.Value;
        }
        public DataContext CreateDbContext(string[] args)
        {
            var configs = _configs;
            
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(configs.DefaultConnectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}