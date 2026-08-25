using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Domain.Tests
{
    public class DataBaseContext : DbContext
    {
        public DbSet <AggregatedMachineDatas> AggregatedMachineDatas { get; set; }
        public DbSet <Machine> Machine { get; set; }
        public DbSet <MachineDatas> MachineDatas { get; set; }

        public DataBaseContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
