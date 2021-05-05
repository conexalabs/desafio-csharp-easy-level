using System;
using Application.Entidades.Base;

namespace Application.Entidades.City
{
    public class City : EntityBase
    {
        public string CityName { get; set; }
        public virtual Coord coord { get; set; }
        public string Temp { get ; set; }
        public DateTime UltimaAtualizacao { get; set; } 
    }
}