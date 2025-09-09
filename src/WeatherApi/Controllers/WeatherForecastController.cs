using Microsoft.AspNetCore.Mvc;
using Application;
using Domain;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<System.Collections.Generic.List<Domain.WeatherForecast>>> Get()
    {
        var forecasts = await _service.GetForecastsAsync();
        return Ok(forecasts.ToList());
    }
}
