using System.Collections.Generic;
using System.Threading.Tasks;
using Siagri.Clima.Business.Dtos;

namespace Siagri.Clima.Business.Interfaces.Services
{
    public interface ILocalidadeService
    {
        Task<LocalidadeDto> ObterPorCidade(CidadeDto localidadeDto);

        Task<LocalidadeDto> ObterPorCoordenada(CoordenadaDto coordenada);

        Task<HistoricoConsultasDto> ObterPorCidadeOuCoordenada(LocalidadeDto localidadeDto);

        Task<LocalidadeDto> Adicionar(LocalidadeDto localidadeDto);
    }
}
