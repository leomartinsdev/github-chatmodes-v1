using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Application;
using Infrastructure;
using System;

namespace WeatherApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Register controller services
            builder.Services.AddControllers();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Register WeatherForecastService for DI
            builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseCors();
            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
