namespace WebAPI.Domain.Interfaces.Response.CityWeather
{
    public interface ICityWeatherActionResult
    {
        public T TemperatureResponse<T>(T response) where T : class;
    }
}
