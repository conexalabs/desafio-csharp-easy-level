using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.Model
{
    public class CityWeatherModel
    {
        public string CityName { get; set; }
        public float CityTemp { get; set; }
        public DateTimeOffset? Date { get; set; } = DateTimeOffset.UtcNow;
    }
}