using Microsoft.AspNetCore.Http.HttpResults;

namespace server.WeatherForecasts;

public static class WeatherForecastEndpoints
{
    public static void AddWeatherForecastEndpoints(this WebApplication app)
    {
        app.MapGet("/",GetForecast);
    }
    public static async Task<Results<Ok<WeatherForecastsResults>,ProblemHttpResult>> GetForecast()
    {
        var result = new WeatherForecastsResults()
        {
            Time = DateTime.Now,
            TemperatureC = 25
        };
        return TypedResults.Ok(result);
    }
}