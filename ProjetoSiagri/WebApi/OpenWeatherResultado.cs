
namespace WebApi
{
    public class OpenWeatherResultado
    {
        public OpenWeatherMainResultado main { get; set; }

        public OpenWeatherCoordResultado coord { get; set; }

        public int id { get; set; }
        public string name { get; set; }       
        
    }

    public class OpenWeatherMainResultado
    {
        public double temp { get; set; }
    }

    public class OpenWeatherCoordResultado
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}
