namespace Siagri.Clima.Business.Constants
{
    public class SiagriSettings
    {
        public static SiagriSettings Instance { get; private set; }

        public static string ApiKey { get; set; }

        public static string OpenWeatherUrl { get; set; }

        public static void Initialize(SiagriSettings appSettings)
        {
            Instance = appSettings;
        }
    }
}
