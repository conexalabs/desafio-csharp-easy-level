using System.Collections.Generic;
using ClimaAPI.Domain.Entities;
using ClimaAPI.Domain.Interfaces.Repositories;
using ClimaAPI.Domain.Interfaces.Services;

namespace ClimaAPI.Domain.Services
{
    public class ServiceRegistro : ServiceBase<Registro>, IServiceRegistro
    {
        private readonly IRepositoryRegistro _repositoryRegistro;

        public ServiceRegistro(IRepositoryRegistro repositoryRegistro)
            : base(repositoryRegistro)
        {
            _repositoryRegistro = repositoryRegistro;
        }

        public IEnumerable<Registro> GetHistorico(int cidadeId)
        {
            return _repositoryRegistro.GetHistorico(cidadeId);
        }
    }
}
