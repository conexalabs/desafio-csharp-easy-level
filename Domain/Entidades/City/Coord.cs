using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entidades.Base;

namespace Domain.Entidades.City
{
    [NotMapped]
    public class Coord : EntityBase
    {
        public string lat { get; set; }
        public string lon { get; set; }
    }
}