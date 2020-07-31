using desafio_conexa.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa
{
    public class Retorno
    {
        public string Mensagem { get; set; }
        public decimal? temperatura { get; set; }
        public List<HistoricoRetorno> Historico { get; set; }
        [JsonIgnore]
        public bool Sucesso { get; set; }
    }

    public class HistoricoRetorno
    {
        public DateTime Data { get; set; }
        public decimal Temperatura { get; set; }
    }
}
