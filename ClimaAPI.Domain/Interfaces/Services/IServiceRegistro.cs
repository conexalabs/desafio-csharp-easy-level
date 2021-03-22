using System.Collections.Generic;
using ClimaAPI.Domain.Entities;

namespace ClimaAPI.Domain.Interfaces.Services
{
    public interface IServiceRegistro : IServiceBase<Registro>
    {
        IEnumerable<Registro> GetHistorico(int cidadeId);
    }
}