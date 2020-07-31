using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa.Repository
{
    [Table("Cidades")]
    public class Cidade
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
