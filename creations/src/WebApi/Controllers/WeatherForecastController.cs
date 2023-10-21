using Creations.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace Creations.WebApi.Controllers;
public class WeatherForecastController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Creations.Application.WeatherForecasts.Queries.GetWeatherForecasts.WeatherForecast>> Get()
    {
        return await Mediator.Send(new Creations.Application.WeatherForecasts.Queries.GetWeatherForecasts.GetWeatherForecastQuery());
    }
}
