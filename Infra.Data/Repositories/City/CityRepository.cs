using Application.Entidades.City;
using Infra.Data.DBContext;
using Infra.Data.Repositories.Base;

namespace Infra.Data.Repositories
{
    public class CityRepository : RepositoryBase<City>
    {
        public CityRepository(ConexaContext conexadb) : base(conexadb)
        {
        }
    }
}