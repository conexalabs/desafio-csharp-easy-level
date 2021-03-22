using ClimaAPI.Domain.Entities;
using ClimaAPI.Domain.Interfaces.Repositories;
using ClimaAPI.Domain.Interfaces.Services;

namespace ClimaAPI.Domain.Services
{
    public class ServiceCidade : ServiceBase<Cidade>, IServiceCidade
    {
        private readonly IRepositoryCidade _repositoryCidade;

        public ServiceCidade(IRepositoryCidade repositoryCidade)
            : base(repositoryCidade)
        {
            _repositoryCidade = repositoryCidade;
        }

        public Cidade GetByName(string cidadeNome)
        {
            return _repositoryCidade.GetByName(cidadeNome);
        }

        public Cidade GetByCoordinates(double latitude, double longitude)
        {
            return _repositoryCidade.GetByCoordinates(latitude, longitude);
        }
    }
}
