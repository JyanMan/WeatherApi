using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers;

[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("weather-forecast")]
    public async Task<ForecastMain> GetWeatherForecast(string apikey, string location = "Manila")
    {
        JsonSerializerOptions jsonOptions =  new() { PropertyNameCaseInsensitive = true, WriteIndented = true };
        var apiKey = Environment.GetEnvironmentVariable("WEATHER_API_KEY") ??
            throw new Exception("apikey environment variable is not set, set your weather api key to WEATHER-API-KEY environment variable");

        ApiHandler api = new() { url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{ location },PH?key={ apiKey }" };
        HttpResponseMessage response = await api.SendRequest();
        StreamReader reader = new(response.Content.ReadAsStream());

        string content = reader.ReadToEnd();
        var jsonContent = JsonSerializer.Deserialize<ForecastMain>(content, jsonOptions) ??
            throw new HttpRequestException("json is null");

        return jsonContent;
    }

    [HttpGet]
    [Route("weather-forecast/current-conditions")]
    public async Task<CurrentConditions> GetCurrentConditions(string location = "Manila")
    {
        ForecastMain forecast = await GetWeatherForecast(location);
        return forecast.CurrentConditions ?? throw new HttpRequestException("null forecast");
    }
}