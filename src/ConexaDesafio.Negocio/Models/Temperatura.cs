using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConexaDesafio.Negocio.Models
{
    public class Temperatura : Entidade
    {
        public int TemperaturaId { get; set; }
        
        public string TemperaturaCelsius { get; set; }

        public DateTime Data { get; set; }

        public int CidadeId { get; set; }

        #region EF RELAÇÃO
        public CidadeTemperatura Cidade { get; set; }
        #endregion
    }
}