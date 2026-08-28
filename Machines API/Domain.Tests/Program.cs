using Domain.Tests.DBContexts;
using Microsoft.EntityFrameworkCore;

namespace Domain.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration
                    .GetConnectionString("CacheServerHost");
            });

            string dataBaseConnectionString = builder.Configuration.GetConnectionString("DataBaseConnectionString");
            builder.Services.AddDbContext<MachinesDBContext>((serviceProvider, optionsBuilder) =>
                optionsBuilder.UseSqlServer(dataBaseConnectionString));
            builder.Services.AddDbContext<MachinesDatasDBContext>((serviceProvider, optionsBuilder) =>
                optionsBuilder.UseSqlServer(dataBaseConnectionString));
            builder.Services.AddDbContext<AggregatedMachinesDatasDBContext>((serviceProvider, optionsBuilder) =>
                optionsBuilder.UseSqlServer(dataBaseConnectionString));
            
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
