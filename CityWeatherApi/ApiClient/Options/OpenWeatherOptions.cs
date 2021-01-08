using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityWeatherApi.ApiClient.Options
{
    public class OpenWeatherOptions
    {
        private string _baseAddress;

        public string BaseAddress
        {
            get { return _baseAddress; }
            set { _baseAddress = value; }
        }

        private string _cityEndPoint;

        public string CityEndPoint
        {
            get { return _cityEndPoint; }
            set { _cityEndPoint = value; }
        }

        public string CityUrl => $"{BaseAddress}/{CityEndPoint}";

        private string _latLonEndPoint;

        public string LatLonEndPoint
        {
            get { return _latLonEndPoint; }
            set { _latLonEndPoint = value; }
        }

        public string LatLonUrl => $"{BaseAddress}/{LatLonEndPoint}";

        private string _historicEndPoint;
        public string HistoricEndPoint
        {
            get { return _historicEndPoint; }
            set { _historicEndPoint = value; }
        }

        public string HistoricUrl => $"{BaseAddress}/{HistoricEndPoint}";
    }
}
