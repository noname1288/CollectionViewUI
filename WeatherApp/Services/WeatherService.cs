using System.Net.Http.Json;
using WeatherApp.Models.Responses;

namespace WeatherApp.Services;

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private const string BASE_URL = "https://api.openweathermap.org/data/2.5/";
    private const string API_KEY = "4390c2f6360e65f5600494aaa2aa42d2";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<GetCurrentWeatherResponse> GetWeatherByLatLon(string lat, string lon)
    {
        if (lat == null || lon == null)
            return null;
        // https://api.openweathermap.org/data/2.5/weather?lat=44.34&lon=10.99&appid=4390c2f6360e65f5600494aaa2aa42d2
        var url = $"{BASE_URL}weather?lat={lat}&lon={lon}&appid={API_KEY}";
        return await _httpClient.GetFromJsonAsync<GetCurrentWeatherResponse>(url);
    }

    public Task<GetCurrentWeatherResponse> GetWeatherByCityName(string city)
    {
        if (string.IsNullOrEmpty(city))
            return  null;

        return null;
    }
}