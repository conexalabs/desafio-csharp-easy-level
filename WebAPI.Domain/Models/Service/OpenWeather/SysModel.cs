using Newtonsoft.Json;
using System;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class SysModel
    {
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("country")]
        public String Country { get; set; }
        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }
        [JsonProperty("sunset")]
        public int Sunset { get; set; }
        
    }
}