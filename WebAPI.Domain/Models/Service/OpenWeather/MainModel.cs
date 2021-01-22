using Newtonsoft.Json;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class MainModel
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public double MininumTemperature { get; set; }
        [JsonProperty("temp_max")]
        public double MaximumTemperature { get; set; }
        [JsonProperty("pressure")]
        public int Pressure { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }
}