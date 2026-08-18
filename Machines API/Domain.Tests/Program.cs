namespace Domain.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string? connectionString = builder.Configuration
            .GetConnectionString("DataBaseConnectionString");

            builder.Services.AddTransient<DataBaseContext>
                ((IServiceProvider serviceProvider) => new DataBaseContext(connectionString));
            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
