using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.Interfaces.Service.Base;
using Application.ViewModels;
using Application.ViewModels.City.Response;

namespace Application.Interfaces.Service
{
    public interface ICityService : IBaseService<CityViewModelRequest, CityViewModelResponse, City>
    {
        Task<CityViewModelResponse> GetTempCidade(string Cidade);
        Task<CityViewModelResponse> GetTempCoord(string lon, string lat);
        IList<CityViewModelResponse> GetAll();

    }
}