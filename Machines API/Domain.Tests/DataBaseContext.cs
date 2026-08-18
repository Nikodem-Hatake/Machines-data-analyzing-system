using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Domain.Tests
{
    public class DataBaseContext : DbContext
    {
        private string? _connectionString;
        public DbSet <AggregatedMachineDatas> AggregatedMachineDatas { get; set; }
        public DbSet <Machine> Machine { get; set; }
        public DbSet <MachineDatas> MachineDatas { get; set; }

        public DataBaseContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
