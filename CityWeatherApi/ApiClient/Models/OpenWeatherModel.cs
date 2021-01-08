using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Models
{
    public class OpenWeatherModel
    {
        public OpenWeatherCoordinateModel coord { get; set; }
        public OpenWeatherWeatherModel[] weather { get; set; }
        public string _base { get; set; }
        public OpenWeatherMainModel main { get; set; }
        public int visibility { get; set; }
        public OpenWeatherWindModel wind { get; set; }
        public OpenWeatherCloudsModel clouds { get; set; }
        public int dt { get; set; }
        public OpenWeatherSysModel sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }
}
