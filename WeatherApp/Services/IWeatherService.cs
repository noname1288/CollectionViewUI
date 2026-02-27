using WeatherApp.Models.Responses;

namespace WeatherApp.Services;

public interface IWeatherService
{
    Task<GetCurrentWeatherResponse> GetWeatherByLatLon(string lat, string lon);
    Task<GetCurrentWeatherResponse> GetWeatherByCityName(string city);
}