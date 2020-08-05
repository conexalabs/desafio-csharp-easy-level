using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ConexaDesafio.Negocio.Models
{
    public class CidadeTemperatura : Entidade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        //public int CityId { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        #region EF Relação
        public IEnumerable<Temperatura> Temperatura { get; set; }
        #endregion
    }
}
