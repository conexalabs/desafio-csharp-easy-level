using System.Collections.Generic;
using Application.Entidades.City;
using Application.Interfaces.Repository.Base;

namespace Application.Interfaces.Repository
{
    public interface ICityRepository : IBaseRepository<City>
    {
        IList<City> GetAll();
    }
}