using System.Text.Json;
using Domain;
using Application;
using System.Net.Http;

namespace Infrastructure;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;

    // Example coordinates: Berlin
    private const double Latitude = 52.52;
    private const double Longitude = 13.41;

    public WeatherForecastService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<WeatherForecast>> GetForecastsAsync()
    {
        // Removed placeholder comment for clarity.
        var url = $"https://api.open-meteo.com/v1/forecast?latitude={Latitude}&longitude={Longitude}&daily=temperature_2m_max,temperature_2m_min,weathercode&timezone=auto";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();

        var openMeteo = JsonSerializer.Deserialize<OpenMeteoResponse>(json);
        if (openMeteo?.daily == null || openMeteo.daily.time == null || openMeteo.daily.temperature_2m_max == null || openMeteo.daily.weathercode == null)
            return new List<WeatherForecast>();

        var forecasts = new List<WeatherForecast>();
        int count = new[] {
            openMeteo.daily.time.Length,
            openMeteo.daily.temperature_2m_max.Length,
            openMeteo.daily.weathercode.Length
        }.Min();

        for (int i = 0; i < count; i++)
        {
            var date = DateOnly.Parse(openMeteo.daily.time[i]);
            var tempC = (int)openMeteo.daily.temperature_2m_max[i];
            var summary = WeatherCodeToSummary(openMeteo.daily.weathercode[i]);
            forecasts.Add(new WeatherForecast
            {
                Date = date,
                TemperatureC = tempC,
                Summary = summary
            });
        }
        return forecasts;
    }

    private string WeatherCodeToSummary(int code)
    {
        // Simple mapping, see Open-Meteo docs for full codes
        return code switch
        {
            0 => "Clear sky",
            1 or 2 or 3 => "Mainly clear, partly cloudy, and overcast",
            45 or 48 => "Fog",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            71 or 73 or 75 => "Snow",
            80 or 81 or 82 => "Rain showers",
            95 => "Thunderstorm",
            _ => "Unknown"
        };
    }

    private class OpenMeteoResponse
    {
        public Daily? daily { get; set; }
    }
    private class Daily
    {
        public string[]? time { get; set; }
        public double[]? temperature_2m_max { get; set; }
        public double[]? temperature_2m_min { get; set; }
        public int[]? weathercode { get; set; }
    }
}
