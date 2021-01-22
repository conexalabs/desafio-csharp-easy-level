using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Domain.Models.Service.OpenWeather
{
    public class CurrentModel
    {
        [JsonProperty("coord")]
        public CoordinatesModel Coordinate { get; set; }
        [JsonProperty("weather")]
        public List<WeatherModel> Weathers { get; set; }
        [JsonProperty("base")]
        public String Base { get; set; }
        [JsonProperty("main")]
        public MainModel Main { get; set; }
        [JsonProperty("visibility")]
        public int Visibility { get; set; }
        [JsonProperty("wind")]
        public WindModel Wind { get; set; }
        [JsonProperty("clouds")]
        public CloudsModel Clouds { get; set; }
        [JsonProperty("dt")]
        public long Date { get; set; }
        [JsonProperty("sys")]
        public SysModel Sys { get; set; }
        [JsonProperty("timezone")]
        public long TimeZone { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("cod")]
        public int Cod { get; set; }
    }
}
