using Domain.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;
using System.Text.Json;

namespace Domain.Tests.DBContexts
{
    public class BaseDBContext <T> : DbContext where T : class
    {
        protected IDistributedCache _distributedCache;
        protected DbSet <T> _table { get; set; }

        public BaseDBContext(DbContextOptions options, IDistributedCache distributedCache)
            : base(options)
        {
            _distributedCache = distributedCache;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
