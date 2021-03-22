namespace ClimaAPI.Application.Dtos
{
    public class OpenWeatherResposta
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Main Main { get; set; }
        public Coord Coord { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
    }

    public class Coord
    {
        public double Lon {get; set; }
        public double Lat { get; set; }
    }
}
