using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace WeatherAPI_Microservice
{
    public class WeatherClient
    {
        private readonly HttpClient httpClient;
        private readonly ServiceSettings settings;

        public WeatherClient(HttpClient httpClient, IOptions<ServiceSettings> options)
        {
            this.httpClient = httpClient;
            settings = options.Value;
        }

        //C#9
        public record Weather(string description);
        public record Main(decimal temp);
        public record Forecast(Weather[] Weather, Main Main, long Dt);

        public async Task<Forecast> GetCurrentWeatherAsync(string cityId)
        {
            var forecast = await httpClient.GetFromJsonAsync<Forecast>($"https://{settings.OpenWeatherHost}/data/2.5/weather?id={cityId}&appid={settings.ApiKey}");

            return forecast;
        }
        
    }
}