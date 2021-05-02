using System.ComponentModel.DataAnnotations.Schema;
using Application.Entidades.Base;

namespace Application.Entidades.City
{
    [NotMapped]
    public class Coord : EntityBase
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
}