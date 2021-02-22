using System;

namespace WebApi
{
    public class DadosTemperatura
    {
        public DateTime DataHoraLeitura { get; set; }

        public double TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string NomeCidade { get; set; }
    }
}
