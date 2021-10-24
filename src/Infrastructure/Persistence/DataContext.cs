using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<LocationDetail> LocationDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<LocationDetail>()
                .HasKey(x => x.Id);
        }
    }
}