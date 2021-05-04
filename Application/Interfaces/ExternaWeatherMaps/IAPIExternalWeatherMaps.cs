using System.Threading.Tasks;
using Application.ViewModels.City.Response;

namespace Application.Interfaces.ExternaWeatherMaps
{
    public interface IApiExternalWeatherMaps
    {
        Task<CityViewModelResponse> GetTempByCity(string cityName);
        Task<CityViewModelResponse> GetTempByLonLat(string lat, string lon);

    }
}