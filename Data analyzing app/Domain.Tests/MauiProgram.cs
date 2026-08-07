using Domain.Tests.MVVM.ViewModels;
using Domain.Tests.MVVM.Views;
using Microsoft.Extensions.Logging;

namespace Domain.Tests
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<DataBaseContext>(new DataBaseContext(GetConnectionString()));
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainView>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static string GetConnectionString()
        {
            string ipAddress = "localhost";
            if(Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
            {
                ipAddress = "10.0.2.2";
            }
            return $"Server={ipAddress},1433;Database=Machines;User Id=sa;Password=abcd1234._.;TrustServerCertificate=True;";
        }
    }
}
