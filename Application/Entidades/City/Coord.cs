using System.ComponentModel.DataAnnotations.Schema;
using Application.Entidades.Base;

namespace Application.Entidades.City
{
    
    public class Coord : EntityBase
    {
        [ForeignKey(nameof(cityId))]
        public virtual int cityId { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
    }
}