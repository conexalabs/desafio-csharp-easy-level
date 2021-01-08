using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Models
{
    public class OpenWeatherMainModel
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }
}
