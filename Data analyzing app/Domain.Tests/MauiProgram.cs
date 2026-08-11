using Domain.Tests.MVVM.ViewModels;
using Domain.Tests.MVVM.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Reflection;

namespace Domain.Tests
{
    public static class MauiProgram
    {
        private static void AddAppSettings(this MauiAppBuilder mauiAppBuilder)
        {
            using Stream stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("Domain.Tests.appsettings.json");

            if(stream != null)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
                mauiAppBuilder.Configuration.AddConfiguration(configuration);
            }
        }

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

            builder.AddAppSettings();

            builder.Services.AddSingleton<DataBaseContext>
            (new DataBaseContext(GetConnectionString(builder)));
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainView>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static string GetConnectionString(MauiAppBuilder builder)
        {
            if(Microsoft.Maui.Devices.DeviceInfo.Platform == DevicePlatform.Android)
            {
                return builder.Configuration.GetValue<string>("DataBaseConnectionStringForAndroid");
            }
            return builder.Configuration.GetValue<string>("DataBaseConnectionString");
        }
    }
}
