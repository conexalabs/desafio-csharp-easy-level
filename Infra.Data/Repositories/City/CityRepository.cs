using System.Collections.Generic;
using System.Linq;
using Application.Entidades.City;
using Application.Interfaces.Repository;
using Infra.Data.DBContext;
using Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

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
            return _conexaContext.Set<City>().Include(x=>x.coord).Where(x=>x.IsDeleted==false).ToList();
        }

        public City GetByCidade(string cidade)
        {
            return _conexaContext.Citys.Include(x=>x.coord).AsTracking().FirstOrDefault(x => x.IsDeleted == false && x.CityName == cidade);
        }

        public bool AnyByCidade(string cidade)
        {
            return _conexaContext.Citys.Any(x => x.CityName == cidade);
        }


        public bool AnyLonLat(string lat, string lon)
        {
            var obj= _conexaContext.Citys.Include(x=>x.coord).Any(x => x.coord.lat==lat && x.coord.lon==lon);
            return obj;
        }

        public City GetByLonLat(string lat, string lon)
        {
            var obj= _conexaContext.Citys.Include(x=>x.coord).FirstOrDefault(x => x.coord.lat==lat && x.coord.lon==lon);
            return obj;
        }

        
    }
    }