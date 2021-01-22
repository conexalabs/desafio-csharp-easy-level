using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Domain.Models.Entities
{
    public class CityWeather
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "A pesquisa não retornou uma cidade!")]
        public String CityName { get; set; }
        [Required]
        public double Temperature { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
