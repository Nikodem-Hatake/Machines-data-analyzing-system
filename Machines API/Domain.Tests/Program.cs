using Microsoft.EntityFrameworkCore;

namespace Domain.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataBaseContext>((serviceProvider, optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(builder.Configuration
                    .GetConnectionString("DataBaseConnectionString"));
            });
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
