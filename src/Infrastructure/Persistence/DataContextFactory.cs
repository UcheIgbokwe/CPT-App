using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("workstation id=KeyPadBankingDb.mssql.somee.com;packet size=4096;user id=ucheigbokwe_SQLLogin_1;pwd=ii4m8p4yfz;data source=KeyPadBankingDb.mssql.somee.com;persist security info=False;initial catalog=KeyPadBankingDb");

            return new DataContext(optionsBuilder.Options);
        }
    }
}