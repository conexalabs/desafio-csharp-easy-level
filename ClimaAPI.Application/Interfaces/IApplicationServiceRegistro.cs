using System.Collections.Generic;
using System.Threading.Tasks;
using ClimaAPI.Application.Dtos;

namespace ClimaAPI.Application.Interfaces
{
    public interface IApplicationServiceRegistro
    {
        Task<RegistroDto> Post(string cidadeNome);
        Task<RegistroDto> Post(double latitude, double longitude);
        IEnumerable<RegistroDto> GetHistorico(string cidadeNome);
        IEnumerable<RegistroDto> GetHistorico(double latitude, double longitude);
    }
}