namespace Domain.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            APIConnectionManager.APIUrl = "http://" + builder
                .Configuration["APIHostName"];
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
