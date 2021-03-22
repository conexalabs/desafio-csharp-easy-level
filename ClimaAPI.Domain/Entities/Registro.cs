using System;

namespace ClimaAPI.Domain.Entities
{
    public class Registro
    {
        public int RegistroId { get; set; }
        public double Temperatura { get; set; }
        public int CidadeId { get; set; }
        public DateTime Data { get; set; }
    }
}
