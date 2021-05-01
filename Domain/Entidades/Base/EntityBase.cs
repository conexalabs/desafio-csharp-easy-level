using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidades.Base
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}