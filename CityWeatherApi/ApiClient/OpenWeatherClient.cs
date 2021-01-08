using CityWeatherApi.ApiClient.Interfaces;
using CityWeatherApi.ApiClient.Models;
using CityWeatherApi.ApiClient.Models.Historic;
using CityWeatherApi.ApiClient.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient
{
    public class OpenWeatherClient : IOpenWeatherClient
    {
        private readonly HttpClient _client;
        private readonly OpenWeatherOptions _options;
        private readonly string OpenWeatherKey = "48389be733a072a3d6b455e37c8ae711";
        private readonly ILogger<OpenWeatherClient> _logger;

        public OpenWeatherClient(ILogger<OpenWeatherClient> logger, HttpClient client, IOptions<OpenWeatherOptions> options)
        {
            _client = client;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<HistoricWeatherModel> GetHistoric(float lat, float lon)
        {
            var response = await _client.GetAsync(string.Format(_options.HistoricUrl, lat, lon, OpenWeatherKey));
            var responseResult = await response.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<HistoricWeatherModel>(responseResult);
            return responseModel;
        }

        public async Task<OpenWeatherModel> GetTempByCityName(string cityName)
        {
            try
            {
                var response = await _client.GetAsync(string.Format(_options.CityUrl, cityName, OpenWeatherKey));
                var responseResult = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<OpenWeatherModel>(responseResult);
                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public async Task<OpenWeatherModel> GetTempByLatLon(float lat, float lon)
        {
            try
            {
                var response = await _client.GetAsync(string.Format(_options.LatLonUrl, lat, lon, OpenWeatherKey));
                var responseResult = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<OpenWeatherModel>(responseResult);
                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }

        }
    }
}
