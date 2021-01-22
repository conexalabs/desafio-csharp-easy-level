using Newtonsoft.Json;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class WindModel
    {
        [JsonProperty("speed")]
        public float Speed { get; set; }
        [JsonProperty("deg")]
        public int Degrees { get; set; }
    }
}