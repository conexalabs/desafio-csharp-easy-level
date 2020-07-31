using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa.Repository
{
    [Table("Temperaturas")]
    public class Temperatura
    {
        [Key]
        public long Id { get; set; }
        public DateTime DataCaptura { get; set; }
        public int IdCidade { get; set; }
        [NotMapped]
        public Cidade CidadeO { get; set; }
        public decimal TemperaturaAt { get; set; }
    }
}
