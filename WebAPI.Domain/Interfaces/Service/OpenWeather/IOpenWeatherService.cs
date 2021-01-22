using WebAPI.Domain.Models.Service.OpenWeather;

namespace WebAPI.Domain.Interfaces.Service.OpenWeather
{
    public interface IOpenWeatherService
    {
        public CurrentModel GetTemperatureByCityName(string cityName);
        public CurrentModel GetTemperatureByGeoCoordinates(string latitude, string longitude);
    }
}
