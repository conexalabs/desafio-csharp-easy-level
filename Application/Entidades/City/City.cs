using System;
using Application.Entidades.Base;

namespace Application.Entidades.City
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
        public string Temp { get; set; }
        public DateTime UltimaAtualizacao { get; set; } 
    }
}