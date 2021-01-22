using Newtonsoft.Json;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class CloudsModel
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }
}