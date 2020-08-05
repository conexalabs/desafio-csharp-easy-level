using ConexaDesafio.Negocio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConexaDesafio.Api.ViewModels
{
    public class CidadeTemperaturaVm
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime Data { get; set; }
        public Temperatura Temperatura { get; set; }

        public IEnumerable<Clima> Clima { get; set; }
        public Coord Coord { get; set; }

        public Main Main { get; set; }
    }

    public class Coord
    {
        public string lon { get; set; }
        public string lat { get; set; }
    }

    public class Clima
    {
        public string Main { get; set; }
        public string Description { get; set; }

    }

    public class Main
    {
        public string Temp { get; set; }
    }
}
