using Domain.Tests.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tests
{
    public class DataBaseContext : DbContext
    {
        private string _connectionString;
        public DbSet<Machine> Machine { get; set; }
        public DbSet<MachineDatas> MachineDatas { get; set; }

        public DataBaseContext(string connectionString) => this._connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(this._connectionString);
            }
            catch(Exception e)
            {
                ExceptionsHandler.LogExceptionToAlertAsync($"Błąd bazy danych: {e.Message}");
            }
        }
    }
}
