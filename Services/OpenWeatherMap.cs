using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using desafio_csharp_easy_level.Models;

namespace desafio_csharp_easy_level.Services {
    public class OpenWeatherMap {
        public string _apiKey { get; private set; }
        public HttpClient _owm_client { get; private set; }

        public OpenWeatherMap()
        {
            _apiKey = Environment.GetEnvironmentVariable("OWM:ApiKey");
            _owm_client = new HttpClient();
            _owm_client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
            _owm_client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }

        public async Task<WeatherForecast> GetWeatherByName(string name) {
            string _query = $"?q={name}";
            WeatherForecast _forecast = await GetWeather(_query);

            return _forecast;
        }

        public async Task<WeatherForecast> GetWeatherByCoords(double lat, double lon) {
            string _query = $"?lat={lat}&lon={lon}";
            WeatherForecast _forecast = await GetWeather(_query);

            return _forecast;
        }

        public async Task<String> GetCityNameByName(string name) {
            string _query = $"?q={name}";
            WeatherForecast _response = await GetWeather(_query);

            return _response.Name;
        }

        public async Task<String> GetCityNameByCoords(double lat, double lon) {
            string _query = $"?lat={lat}&lon={lon}";
            WeatherForecast _response = await GetWeather(_query);

            return _response.Name;
        }

        private async Task<WeatherForecast> GetWeather(string query){
            HttpResponseMessage response = await _owm_client.GetAsync($"{query}&units=metric&lang=pt_br&appid={_apiKey}");
            OwmApiResponse _resObj = null;
            WeatherForecast _forecast = null;

            if (response.IsSuccessStatusCode){
                _resObj = await response.Content.ReadAsAsync<OwmApiResponse>();
            }

            _forecast = new WeatherForecast{ Name = _resObj.name, Temp= _resObj.main.temp, Date = DateTime.Now };
            return _forecast;
        }
    }
}