using Newtonsoft.Json;
using System.Net.Http;

namespace WebAPI.Domain.Helpers
{
    public static class HttpHelper
    {
        public static T ConvertToModel<T>(HttpResponseMessage responseHttp)
        {
            if (!responseHttp.IsSuccessStatusCode) return default;

            var responseContent = responseHttp.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(responseContent);
            return result;
        }
    }
}
