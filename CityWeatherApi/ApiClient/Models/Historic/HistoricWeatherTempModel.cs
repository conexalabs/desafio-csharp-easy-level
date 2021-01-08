using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Models.Historic
{
    public class HistoricWeatherTempModel
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }
}
