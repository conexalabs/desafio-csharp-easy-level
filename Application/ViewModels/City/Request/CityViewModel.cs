using Newtonsoft.Json;

namespace Application.ViewModels.City
{
    public class CityViewModel
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "coord")]
        public Coord coord { get; set; }
        [JsonProperty(PropertyName = "main")]
        public main main { get; set; }
        public class sys
        {
            [JsonProperty(PropertyName = "country")]
            public string country { get; set; }
        }
    }
}    