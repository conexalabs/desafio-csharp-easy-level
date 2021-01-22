using Newtonsoft.Json;
using System;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class WeatherModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("main")]
        public String Main { get; set; }
        [JsonProperty("description")]
        public String Description { get; set; }
        [JsonProperty("icon")]
        public String Icon { get; set; }
    }
}