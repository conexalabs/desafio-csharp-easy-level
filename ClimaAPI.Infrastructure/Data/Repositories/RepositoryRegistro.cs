using System;
using System.Collections.Generic;
using System.Linq;
using ClimaAPI.Domain.Entities;
using ClimaAPI.Domain.Interfaces.Repositories;

namespace ClimaAPI.Infrastructure.Data.Repositories
{
    public class RepositoryRegistro : RepositoryBase<Registro>, IRepositoryRegistro
    {
        private readonly SqlContext _sqlContext;

        public RepositoryRegistro(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public IEnumerable<Registro> GetHistorico(int cidadeId)
        {
            var todos = _sqlContext.Set<Registro>().ToList();

            return todos.Where(registro =>
                registro.CidadeId == cidadeId
                && registro.Data >= DateTime.Now.AddDays(-30)).ToList();
        }
    }
}
