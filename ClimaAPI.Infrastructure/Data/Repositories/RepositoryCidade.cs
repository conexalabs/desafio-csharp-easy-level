using System;
using System.Linq;
using ClimaAPI.Domain.Entities;
using ClimaAPI.Domain.Interfaces.Repositories;

namespace ClimaAPI.Infrastructure.Data.Repositories
{
    public class RepositoryCidade : RepositoryBase<Cidade>, IRepositoryCidade
    {
        private readonly SqlContext _sqlContext;

        public RepositoryCidade(SqlContext sqlContext)
            : base(sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public Cidade GetByName(string cidadeNome)
        {
            return _sqlContext.Set<Cidade>().FirstOrDefault(cidade => cidade.Nome.Equals(cidadeNome));
        }

        public Cidade GetByCoordinates(double latitude, double longitude)
        {
            return _sqlContext.Set<Cidade>().FirstOrDefault(cidade => Math.Abs(cidade.Latitude - latitude) < 0.01
                                                                      && Math.Abs(cidade.Longitude - longitude) < 0.01);
        }
    }
}