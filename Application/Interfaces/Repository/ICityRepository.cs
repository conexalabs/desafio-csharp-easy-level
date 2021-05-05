using System.Collections.Generic;
using Application.Entidades.City;
using Application.Interfaces.Repository.Base;

namespace Application.Interfaces.Repository
{
    public interface ICityRepository : IBaseRepository<City>
    {
        IList<City> GetAll();
        City GetByCidade(string cidade);
        bool AnyByCidade(string cidade);
        City GetByLonLat(string lat, string lon);
        bool AnyLonLat(string lat, string lon);
    }
}