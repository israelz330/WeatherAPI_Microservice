using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherAPI_Microservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherClient _client;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherClient client)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        [Route("{cityId}")]
        public async Task<WeatherForecast> Get(string cityId)
        {
            var forecast = await _client.GetCurrentWeatherAsync(cityId);

            return new WeatherForecast()
            {

                Summary = forecast.Weather[0].description,
                TemperatureC = (int)forecast.Main.temp,
                Date = DateTimeOffset.FromUnixTimeSeconds(forecast.Dt).DateTime
            };
        }
    }
}
