using Microsoft.AspNetCore.Mvc;

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
    [Route("test/{name}/{ID}")]
    public string TestGet(string name, int ID)
    {
        return $"hello {name}, you have clicked {ID} times";
    }
}