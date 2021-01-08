using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Models.Historic
{
    public class HistoricWeatherModel
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public HistoricWeatherCurrentModel current { get; set; }
        public HistoricWeatherHourlyModel[] hourly { get; set; }
        public HistoricWeatherDailyModel[] daily { get; set; }
    }
}
