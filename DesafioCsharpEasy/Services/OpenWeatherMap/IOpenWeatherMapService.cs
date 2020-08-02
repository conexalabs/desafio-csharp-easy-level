namespace DesafioCsharpEasy.Services.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        CurrentModel GetTemperatureByCity(string city);
        CurrentModel GetTemperatureByCoord(string lat, string lon);
    }
}
