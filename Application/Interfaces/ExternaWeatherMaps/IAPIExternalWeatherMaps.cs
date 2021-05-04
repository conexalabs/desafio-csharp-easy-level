using System.Threading.Tasks;
using Application.ViewModels.City;
using Application.ViewModels.City.Response;

namespace Infra.Data.APIExterna
{
    public interface IApiExternalWeatherMaps
    {
        Task<CityViewModelResponse> GetTempByCity(string Cidade, string metric="metric");
        Task<CityViewModelResponse> GetTempByLonLat(string lat, string lon);

        Task<CityViewModelResponse> GetTempMouthByCity(string Cidade, string KeyAPI);
        Task<CityViewModelResponse> GetTempMouthByLonLat(string Cidade, string KeyAPI);
        
    }
}