using System;
using System.Collections.Generic;

namespace DesafioCsharpEasy.Dtos
{
    public class HistoricalPresentation
    {
        public HistoricalPresentation()
        {
            DateTemperature = new List<DateTemperature>();
        }

        public string City { get; set; }
        public List<DateTemperature> DateTemperature { get; set; }
    }

    public class DateTemperature
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public string Latutude { get; set; }
        public string Longitude { get; set; }
    }
}
