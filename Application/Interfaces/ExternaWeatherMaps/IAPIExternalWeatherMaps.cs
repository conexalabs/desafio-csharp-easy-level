using System.Threading.Tasks;
using Application.ViewModels.City;

namespace Infra.Data.APIExterna
{
    public interface IApiExternalWeatherMaps
    {
        Task<CityViewModel> GetTempByCity(string Cidade, string metric="metric");
        Task<CityViewModel> GetTempByLonLat(string lon, string lat);
    }
}