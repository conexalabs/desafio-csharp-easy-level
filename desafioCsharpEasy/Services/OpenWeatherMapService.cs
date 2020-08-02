using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using desafioCsharpEasy.Models;
using Newtonsoft.Json;

namespace desafioCsharpEasy.Services
{
    public class OpenWeatherMapService
    {
        private readonly string _apiKey;
        private readonly HttpClient _client;

        public OpenWeatherMapService()
        {
            _apiKey = "pleaseyouseyourownkeymybuddy";
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public async Task<WeatherResponse> GetWeatherByCityName(string city)
        {
            return await GetWeather($"q={city}");
        }

        public async Task<WeatherResponse> GetWeatherByCityId(long id)
        {
            return await GetWeather($"id={id}");
        }

        public async Task<WeatherResponse> GetWeatherByCoordinates(double latitude, double longitude)
        {
            return await GetWeather($"lat={latitude}&lon={longitude}");
        }

        private async Task<WeatherResponse> GetWeather(string arg)
        {
            WeatherResponse weatherResponse = null;

            // `units=metric` faz com que a resposta siga o sistema de métrica internacional.
            var response = await _client.GetAsync($"weather?{arg}&units=metric&appid={_apiKey}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(json);
            }

            return weatherResponse;
        }
    }
}