using System.Collections.Generic;
using System.Linq;

namespace Siagri.Clima.Business.Dtos
{
    public class HistoricoConsultasDto
    {
        public IList<LocalidadeDto> Historico { get; set; } 

        public HistoricoConsultasDto(IEnumerable<LocalidadeDto> localidades)
        {
            Historico = localidades.ToList();
        }
    }
}
