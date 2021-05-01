using Domain.Entidades.Base;

namespace Domain.Entidades.City
{
    public class City : EntityBase
    {
        public string Name { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public Coord coord { get; set; }
    }
}