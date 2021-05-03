using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Entidades.City;
using Application.Interfaces.Service.Base;
using Application.ViewModels.City;
using Application.ViewModels.City.Response;

namespace Application.Interfaces.Service
{
    public interface ICityService : IBaseService<CityViewModel, City>
    {
        Task<CityViewModelResponse> GetTempCidade(string Cidade);
        IList<CityViewModel> GetAll();

    }
}