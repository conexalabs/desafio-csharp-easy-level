using System;
using Siagri.Clima.Business.VOs;

namespace Siagri.Clima.Business.Entities
{
    public class Localidade
    {
        public Localidade()
        {
            LocalidadeId = Guid.NewGuid();
        }

        public Guid LocalidadeId { get; set; }

        public CidadeVO Cidade { get; set; }

        public CoordenadaVO Coordenada { get; set; }

        public string TemperaturaAtual { get; set; }

        public DateTime DataConsulta { get; set; }
    }
}
