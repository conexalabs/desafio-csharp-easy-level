using System;

namespace ClimaAPI.Application.Dtos
{
    public class RegistroDto
    {
        public string CidadeNome { get; set; }
        public double Temperatura { get; set; }
        public DateTime Data { get; set; }
    }
}