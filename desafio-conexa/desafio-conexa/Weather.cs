using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace desafio_conexa
{
    public class Weather
    {
        public Main main { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }

    }
    public class Main
    {
        public decimal Temp { get; set; }
    }
}
