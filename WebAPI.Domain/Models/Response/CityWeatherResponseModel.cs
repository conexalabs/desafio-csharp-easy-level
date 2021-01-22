using WebAPI.Domain.Models.Entities;

namespace WebAPI.Domain.Models.Response
{
    public class CityWeatherResponseModel
    {
        public double Temperature { get; set; }

        public CityWeatherResponseModel ModelToResponse<T>(T model) where T : CityWeather
        {
            Temperature = model.Temperature;
            return this;
        }
    }
}
