using Microsoft.EntityFrameworkCore;

namespace Domain.Tests
{
    public class MachinesDatasDBContext : DbContext
    {
        private string _connectionString;
        public DbSet<MachineData> MachineDatas { get; private set; }

        public MachinesDatasDBContext(string connectionString) => this._connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }
    }
}
