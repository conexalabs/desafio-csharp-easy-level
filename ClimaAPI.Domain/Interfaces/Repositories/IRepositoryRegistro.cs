using System.Collections.Generic;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Domain.Interfaces.Repositories
{
    public interface IRepositoryRegistro : IRepositoryBase<Registro>
    {
        IEnumerable<Registro> GetHistorico(int cidadeId);
    }
}
