
using WebAPI.Domain.Models.Response;
using WebAPI.Domain.Interfaces.Response.CityWeather;

namespace WebAPI.Domain.Services.ActionResult
{
    public class CityWeatherActionResult : ICityWeatherActionResult
    {
        public T TemperatureResponse<T>(T response) where T : class
        {
            return response;
        }
    }
}
