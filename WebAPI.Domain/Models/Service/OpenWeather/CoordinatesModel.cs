using Newtonsoft.Json;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class CoordinatesModel
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
    }
}