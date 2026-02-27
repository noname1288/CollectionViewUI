using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Services;

namespace WeatherApp.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly IWeatherService _weatherService;

    [ObservableProperty] private string _weather;
    [ObservableProperty] private string _temperature;
    [ObservableProperty] private string _humidity;
    [ObservableProperty] private bool isLoading;

    public MainPageViewModel(IWeatherService weatherService)
    {
        _weatherService = weatherService;
        LoadData();
    }

    private void LoadData()
    {
        Weather = "Weather";
        Temperature = "Temperature";
        Humidity = "Humidity";
    }

    [RelayCommand]
    private async Task GetDefaultWeather()
    {
        isLoading = false;

        try
        {
            isLoading = true;

            var result = await _weatherService.GetWeatherByLatLon("44.34", "10.99");
            Console.WriteLine(result);
            var weather = result.Weather[0];
            var main = result.Main;
            SetData(weather.Description, main.Temp.ToString("F1"), main.Humidity.ToString() );
            
            
        }
        catch (Exception e)
        {
            await Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
        }
        finally
        {
            isLoading = false;
        }
    }

    private void SetData(string weather, string temperature, string humidity)
    {
        Weather = weather;
        Temperature =  temperature;
        Humidity = humidity;
    }
}