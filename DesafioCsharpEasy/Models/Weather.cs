using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioCsharpEasy.Models
{
	public class Weather
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long CityId { get; set; }
        
        [NotMapped]
        public City City { get; set; }
        public double Temperature { get; set; }
    }
}