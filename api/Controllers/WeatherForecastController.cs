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
    public ForecastMain GetWeatherForecast(string location = "Manila")
    {
        JsonSerializerOptions jsonOptions =  new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

        ApiHandler api = new() { url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{location},PH?key=LMQXJUVLABF5H7VBE7MA4XFBP" };
        HttpResponseMessage response = api.SendRequest();
        StreamReader reader = new(response.Content.ReadAsStream());

        string content = reader.ReadToEnd();
        var jsonContent = JsonSerializer.Deserialize<ForecastMain>(content, jsonOptions);
        
        if (jsonContent == null)
            throw new HttpRequestException("json is null");

        return jsonContent;
    }

    [HttpGet]
    [Route("weather-forecast/current-conditions")]
    public CurrentConditions GetCurrentConditions(string location = "Manila")
    {
        ForecastMain forecast = GetWeatherForecast(location);
        return forecast.CurrentConditions ?? throw new HttpRequestException("null forecast");
    }
}