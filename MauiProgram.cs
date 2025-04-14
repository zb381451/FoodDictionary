using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;
using FoodDictionary.Services;

namespace FoodDictionary
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .ConfigureSyncfusionToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<AppState>(); // theme state

#if DEBUG
            builder.Services.AddSingleton<DatabaseService>();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

