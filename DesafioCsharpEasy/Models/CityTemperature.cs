using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioCsharpEasy.Models
{
    public class CityTemperature
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public double Temperature { get; set; }
    }
}
