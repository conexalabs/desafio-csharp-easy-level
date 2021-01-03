namespace desafio_csharp_easy_level.Models {
    public class OwmApiResponse {
        public string name { get; set; }
        public OwmApiResponseCoord coord { get; set; }
        public OwmApiResponseMain main { get; set; }
    }

    public class OwmApiResponseMain {
        public double temp { get; set; }
    }

    public class OwmApiResponseCoord {
        public double lat { get; set; }
        public double lon { get; set; }
    }
}