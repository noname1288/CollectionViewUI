using Microsoft.Extensions.Logging;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using WeatherApp.Views;

namespace WeatherApp;

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
        //Service
        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<IWeatherService, WeatherService>();
        //ViewModel
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<ListSomeThingPageViewModel>();
        //UI
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ListSomethingPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}