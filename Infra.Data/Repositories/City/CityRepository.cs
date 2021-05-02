using System.Collections.Generic;
using System.Linq;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Infra.Data.DBContext;
using Infra.Data.Repositories.Base;

namespace Infra.Data.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {

        private readonly ConexaContext _conexaContext;
        public CityRepository(ConexaContext conexadb, ConexaContext conexaContext) : base(conexadb)
        {
            _conexaContext = conexaContext;
        }

        public IList<City> GetAll()
        {
            return _conexaContext.Set<City>().Where(x=>x.IsDeleted==false).ToList();
        }
    }
}