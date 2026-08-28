using System.Text.Json;

namespace Domain.Tests
{
    public static class APIConnectionManager
    {
        public static string APIUrl { get; set; }

        public static async Task<T?> Get<T>(string endPoint) where T : class
        {
            try
            {
                using(HttpClient httpClient = new HttpClient())
                {
                    return JsonSerializer.Deserialize<T>
                        (await httpClient.GetStringAsync($"{APIUrl}{endPoint}"),
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                }
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
