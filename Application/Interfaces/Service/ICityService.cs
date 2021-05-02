using System.Collections.Generic;
using Application.Entidades.City;
using Application.Interfaces.Service.Base;
using Application.ViewModels.City;

namespace Application.Interfaces.Service
{
    public interface ICityService : IBaseService<CityViewModel, City>
    {
        CityViewModel GetTempCidade();
        IList<CityViewModel> GetAll();

    }
}