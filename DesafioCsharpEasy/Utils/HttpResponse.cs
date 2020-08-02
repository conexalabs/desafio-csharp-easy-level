using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace DesafioCsharpEasy.Utils
{
    public static class HttpResponse
    {
        public static T ConvertToObj<T>(HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    return default;
                }

                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<T>(content);

                return result;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
