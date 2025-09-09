using Domain;
namespace Application;

public interface IWeatherForecastService
{
    Task<IEnumerable<WeatherForecast>> GetForecastsAsync();
}
