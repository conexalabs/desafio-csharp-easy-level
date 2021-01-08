using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Models.Historic
{
    public class HistoricWeatherDailyModel
    {


        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public HistoricWeatherTempModel temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float dew_point { get; set; }
        public float wind_speed { get; set; }
        public int wind_deg { get; set; }
        public HistoricWeatherWeatherModel[] weather { get; set; }
        public int clouds { get; set; }
        public float pop { get; set; }
        public float rain { get; set; }
        public float uvi { get; set; }

    }
}
